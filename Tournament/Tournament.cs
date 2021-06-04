using Matches;
using People;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Kopakabana
{
    public class Tournament
    {
        private List<Referee> Referees { get; set; }
        private List<Team> Teams { get; set; }
        private List<Match> Matches { get; set; }
        public VolleyBall Semifinal1, Semifinal2, Final;
        public Tournament()
        {
            Referees = new List<Referee>();
            Teams = new List<Team>();
            Matches = new List<Match>();

        }///Ready

        public List<Team> getTop4()
        {
            Teams.Sort((x, y) => y.getWins().CompareTo(x.getWins()));
            List<Team> best4 = new List<Team>(Teams.Take(4));
            return best4;
        }///Ready
        public List<Referee> Get3Referees()
        {
            int Ref, AS1, AS2;
            Random Rn = new Random();
            Ref = Rn.Next(0, Referees.Count);
            while (true)
            {
                AS1 = Rn.Next(0, Referees.Count);
                if (AS1 != Ref)
                    break;
            }
            while (true)
            {
                AS2 = Rn.Next(0, Referees.Count);
                if (AS2 != AS1 && AS2 != Ref)
                    break;
            }
            List<Referee> Trojka = new List<Referee>();
            Trojka.Add(Referees[Ref]);
            Trojka.Add(Referees[AS1]);
            Trojka.Add(Referees[AS2]);
            return Trojka;
        }

        //GENEROWANIE//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Meczów każdy z każdym
        public void GenerateMatches()
        {
            try
            {
                if (Referees.Count < 3)
                    throw new Exception();
                int q = 0, Ref, AS1, AS2;
                Matches = new List<Match>();
                Random Rn = new Random();
                ///Generowanie dla siatkowki
                foreach (Team T in Teams)
                {
                    for (int i = q + 1; i < Teams.Count;)
                    {
                        Ref = Rn.Next(0, Referees.Count);
                        AS1 = Rn.Next(0, Referees.Count);
                        AS2 = Rn.Next(0, Referees.Count);
                        if (Ref != AS1 && Ref != AS2 && AS1 != AS2)
                        {
                            Matches.Add(new VolleyBall(T, Teams[i], Referees[Ref], Referees[AS1], Referees[AS2]));
                            i++;
                        }
                    }
                    q++;
                }
                ///Genereownaie dla Przeciagania liny
                q = 0;
                if (Referees.Count < 1)
                    throw new Exception();
                foreach (Team T in Teams)
                {
                    for (int i = q + 1; i < Teams.Count; i++)
                    {
                        Ref = Rn.Next(0, Referees.Count);
                        Matches.Add(new TugOfWar(T, Teams[i], Referees[Ref]));
                    }
                    q++;
                }
                ///Generwoanie dla Dwoch ogni
                q = 0;
                if (Referees.Count < 1)
                    throw new Exception();
                foreach (Team T in Teams)
                {
                    for (int i = q + 1; i < Teams.Count; i++)
                    {
                        Ref = Rn.Next(0, Referees.Count);
                        Matches.Add(new DodgeBall(T, Teams[i], Referees[Ref]));
                    }
                    q++;
                }
                GenerateSemifinals();
                ReadScore();
                //GenerateFinal();
                /*if (Semifinal1 != null)
                    Matches.Add(Semifinal1);
                if (Semifinal2 != null)
                    Matches.Add(Semifinal2);*/
            }
            catch
            {

            }
        }///Ready
            //Meczów półfinałowych
        public void GenerateSemifinals()
        {
            ReadScore();
            bool whatis = false;
            var top4 = getTop4();
            var RefThree = Get3Referees();
            Semifinal1 = new VolleyBall(top4[0], top4[2], RefThree[0], RefThree[1], RefThree[2]) { WhatSemi = true };
            RefThree = Get3Referees();
            Semifinal2 = new VolleyBall(top4[1], top4[3], RefThree[0], RefThree[1], RefThree[2]) { WhatSemi = true };
            int i = 0;
            foreach (Match M in Matches)
            {
                if (M is VolleyBall)
                {
                    if (M.WhatSemi == true)
                    {
                        Matches[i] = Semifinal1;
                        Matches[i + 1] = Semifinal2;
                        whatis = true;
                        break;
                    }

                }
                i++;
            }
            if (whatis == false)
            {
                if (Semifinal1 != null)
                    Matches.Add(Semifinal1);
                if (Semifinal2 != null)
                    Matches.Add(Semifinal2);
            }
        }
        //Finału
        public void GenerateFinal()
        {
            try
            {
                bool whatis = false;
                int i = 0;
                if (Semifinal1.getWhoWon() != null && Semifinal2.getWhoWon() != null)
                {
                    var RefThree = Get3Referees();
                    Final = new VolleyBall(Semifinal1.getWhoWon(), Semifinal2.getWhoWon(), RefThree[0], RefThree[1], RefThree[2]) { WhatFinal = true };
                    foreach (Match M in Matches)
                    {
                        if (M is VolleyBall)
                        {
                            if (M.WhatFinal == true)
                            {
                                Matches[i] = Final;
                                whatis = true;
                                break;
                            }

                        }
                        i++;
                    }
                }
                else throw new Exception();
                if (whatis == false)
                {
                    if (Final != null)
                        Matches.Add(Final);
                }

            }
            catch { }
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        //OBLICZNIE WYGRANYCH//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void CountWins()
        {
            //Ustawianie wygranych na zero aby przy ponowym liczeniu liczylo od zera
            foreach (Team T in Teams)
            {
                T.setWins(0);
            }
            //Liczenie wygranych 
            foreach (Match M in Matches)
            {
                foreach (Team T in Teams)
                {
                    if (M.getWhoWon() != null)
                        if (M.getWhoWon().getName() == T.getName() && M.WhatSemi==false && M.WhatFinal == false)
                        {
                            T.setWins();
                        }
                }
            }
        }///Ready
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        //ZARZĄDZANIE//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Druzynami
        public void ChangeName(string pop, string nast)
        {
            foreach (Match M in Matches)
            {
                if (M.getTeam1().getName() == pop)
                {
                    M.getTeam1().setName(nast);
                }
                if (M.getTeam2().getName() == pop)
                {
                    M.getTeam2().setName(nast);
                }
            }
        }///Ready
        public void SearchName(string akt)
        {
            foreach (Match M in Matches)
            {
                if (M.getTeam1().getName() == akt)
                {
                    SaveScore(M);
                }
                if (M.getTeam2().getName() == akt)
                {
                    SaveScore(M);
                }
            }
        }///Ready
        public void CheckName(string name)
        {
            foreach (Team T in Teams)
            {
                if (T.getName() == name)
                    throw new ExistNameException(" juz istnieje", name);
            }
        }///Ready
            //Sedziami
        public void ChangeRef(string popname, string popsur, string name, string surname)
        {
            foreach (Referee R in Referees)
            {
                if (R.getName() == popname && R.getSurname() == popsur)
                {
                    R.setName(name);
                    R.setSurname(surname);
                }
            }
        }///Ready
        public int SearchRef(string name, string surname)
        {
            foreach (Referee R in Referees)
            {
                /*if (R.getName() == name && R.getSurname() == surname)
                 {
                     return Referees.IndexOf(R);
                 }*/

                if (R.Equals(new Referee(name, surname)))
                {
                    return Referees.IndexOf(R);

                }
            }
            return 0;

        }///Ready
        public void CheckRef(string name, string surname)
        {
            foreach (Referee R in Referees)
            {
                if (R.getName() == name && R.getSurname() == surname)
                {
                    throw new ExistNameException(" jest zarejetorwanym sedzią", name, surname);
                }
            }
        }///Ready
        public bool WhatExistRef(string name, string surname)
        {
            foreach (Referee R in Referees)
            {
                if (R.Equals(new Referee(name, surname)))
                {
                    return true;

                }
            }
            return false;
        }///Ready
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////



        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///GETTERY I SETTERY/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //USTAWIANIE LIST////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Edycja sedziow
        public void setReferees(int index, string name, string surname)
        {
            Referees[index] = new Referee(name, surname);
        }///Ready
            //Edycja danych w meczach
        public void UpdateMatch(string popname, string popsur, string name, string surname, int index)
        {
            foreach (Match M in Matches)
            {
                bool whatchange = false;
                if (M.GetReferee().Equals(new Referee(popname, popsur)))
                {
                    M.SetReferee(Referees[index]);
                    whatchange = true;
                }
                if (M is VolleyBall)
                {
                    if (((VolleyBall)M).GetAssistant1().Equals(new Referee(popname, popsur)))
                    {
                        ((VolleyBall)M).SetAssistant1(Referees[index]);
                        whatchange = true;
                    }
                    if (((VolleyBall)M).GetAssistant2().Equals(new Referee(popname, popsur)))
                    {
                        ((VolleyBall)M).SetAssistant2(Referees[index]);
                        whatchange = true;
                    }
                }
                if (whatchange)
                    SaveScore(M);
            }
        }///Ready
            //Edycja Wyników
        public void setResult(int index, int res1, int res2)
        {
            var M = Matches[index];
            Matches[index].setResult1(res1);
            Matches[index].setResult2(res2);

        }///Ready
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////





        //DANE///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Odczyt z plików////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void Read()
        {
            string R = "Referees.txt", Te = "Teams.txt";
            try
            {
                string linia, imie = "", nazwisko = "";
                int i = 0, j, x, y, z;
                if (File.Exists(R))
                {
                    StreamReader Ref = new StreamReader(R);

                    while ((linia = Ref.ReadLine()) != null)
                    {
                        for (i = 0; linia[i] != ' '; i++)
                        {
                            imie += linia[i];
                        }
                        for (j = i + 1; linia[j] != ' '; j++)
                        {
                            nazwisko += linia[j];
                        }
                        Referees.Add(new Referee(imie, nazwisko));
                        imie = "";
                        nazwisko = "";

                    }
                    Ref.Close();
                }
                else throw new FileException("Nie odnaleziono okreslonej ściezki\n", Path.GetFullPath(R));

                if (File.Exists(Te))
                {
                    StreamReader T = new StreamReader(Te);
                    string nazwa = "";
                    while ((linia = T.ReadLine()) != null)
                    {
                        List<Player> PL = new List<Player>();
                        for (i = 0; linia[i] != ' '; i++)
                        {
                            nazwa += linia[i];
                        }
                        for (j = 0; j < 4; j++)
                        {
                            z = i;
                            for (x = z + 1; linia[x] != ' '; x++)
                            {
                                imie += linia[x];
                            }
                            for (y = x + 1; linia[y] != ' ' && linia[y] != ';'; y++)
                            {
                                nazwisko += linia[y];
                            }
                            PL.Add(new Player(imie, nazwisko));
                            imie = "";
                            nazwisko = "";
                            i = y;
                        }
                        Teams.Add(new Team(nazwa, PL[0], PL[1], PL[2], PL[3]));
                        nazwa = "";
                    }
                    T.Close();
                }
                else throw new FileException("Nie odnaleziono okreslonej ściezki\n", Path.GetFullPath(Te));
            }

            catch (FileException ex)
            {

                MessageBox.Show(ex.Message + ex.getName(), "Error", MessageBoxButton.OK);
            }


        }///Ready
        public void ReadScore()
        {
            try
            {
                string Ma = "Matches.txt";
                if (File.Exists(Ma))
                {
                    StreamReader Mat = new StreamReader(Ma);
                    int i, j, k, l, q, sc1 = 0, sc2 = 0;
                    char type;
                    string linia, name1, name2, score1, score2;
                    while ((linia = Mat.ReadLine()) != null)
                    {
                        string mainname = "", mainsur = "", as1name = "", as1sur = "", as2name = "", as2sur = "";
                        type = linia[0];
                        name1 = ""; name2 = ""; score1 = ""; score2 = "";

                        for (i = 2; linia[i] != ' '; i++)
                        {
                            name1 += linia[i];
                        }
                        for (j = i + 3; linia[j] != ' '; j++)
                        {
                            name2 += linia[j];

                        }
                        j++;
                        score1 += linia[j];
                        j = j + 4;
                        score2 += linia[j];
                        for (k = j + 2; linia[k] != ' '; k++)
                        {
                            mainname += linia[k];
                        }
                        for (l = k + 1; linia[l] != ' '; l++)
                        {
                            if (linia[l] != ';')
                                mainsur += linia[l];
                            else break;
                        }
                        if (type == 'V')
                        {
                            for (q = l + 1; linia[q] != ' '; q++)
                            {
                                as1name += linia[q];

                            }
                            for (i = q + 1; linia[i] != ' '; i++)
                            {
                                as1sur += linia[i];

                            }
                            for (q = i + 1; linia[q] != ' '; q++)
                            {
                                as2name += linia[q];

                            }
                            for (i = q + 1; linia[i] != ';'; i++)
                            {
                                as2sur += linia[i];

                            }
                        }
                        sc1 = int.Parse(score1);
                        sc2 = int.Parse(score2);
                        var Mecze = getMatches();
                        foreach (Match M in Matches)
                        {
                            if (((M is VolleyBall && type == 'V' && M.WhatSemi==false) || (M is VolleyBall && type == 'S' && M.WhatSemi) || (M is VolleyBall && type == 'F' && ((VolleyBall)M).WhatFinal)) && ((M.getTeam1().getName() == name1) && (M.getTeam2().getName() == name2)))
                            {
                                M.setResult1(sc1);
                                M.setResult2(sc2);
                                if (WhatExistRef(mainname, mainsur))
                                    M.SetReferee(getReferees()[SearchRef(mainname, mainsur)]);
                                if (WhatExistRef(as1name, as1sur))
                                    ((VolleyBall)M).SetAssistant1(getReferees()[SearchRef(as1name, as1sur)]);
                                if (WhatExistRef(as2name, as2sur))
                                    ((VolleyBall)M).SetAssistant2(getReferees()[SearchRef(as2name, as2sur)]);
                                M.SetWhoWon();
                                if (M.GetReferee().Equals(((VolleyBall)M).GetAssistant1()) || M.GetReferee().Equals(((VolleyBall)M).GetAssistant2()) || ((VolleyBall)M).GetAssistant1().Equals(((VolleyBall)M).GetAssistant2()))
                                {
                                    int z = 0, Ref, AS1, AS2;
                                    Random Rn = new Random();

                                    Ref = Rn.Next(0, Referees.Count);
                                    while (true)
                                    {
                                        AS1 = Rn.Next(0, Referees.Count);
                                        if (AS1 != Ref)
                                            break;
                                    }
                                    while (true)
                                    {
                                        AS2 = Rn.Next(0, Referees.Count);
                                        if (AS2 != AS1 && AS2 != Ref)
                                            break;
                                    }

                                    M.SetReferee(getReferees()[Ref]);
                                    ((VolleyBall)M).SetAssistant1(getReferees()[AS1]);
                                    ((VolleyBall)M).SetAssistant2(getReferees()[AS2]);

                                }
                            }
                            else if (((M is VolleyBall && type == 'V' && M.WhatSemi == false) || (M is VolleyBall && type == 'S' && ((VolleyBall)M).WhatSemi) || (M is VolleyBall && type == 'F' && ((VolleyBall)M).WhatFinal)) && ((M.getTeam1().getName() == name2) && (M.getTeam2().getName() == name1)))
                            {
                                M.setResult1(sc2);
                                M.setResult2(sc1);
                                if (WhatExistRef(mainname, mainsur))
                                    M.SetReferee(getReferees()[SearchRef(mainname, mainsur)]);
                                if (WhatExistRef(as1name, as1sur))
                                    ((VolleyBall)M).SetAssistant1(getReferees()[SearchRef(as1name, as1sur)]);
                                if (WhatExistRef(as2name, as2sur))
                                    ((VolleyBall)M).SetAssistant2(getReferees()[SearchRef(as2name, as2sur)]);
                                M.SetWhoWon();
                                    if (M.GetReferee().Equals(((VolleyBall)M).GetAssistant1()) || M.GetReferee().Equals(((VolleyBall)M).GetAssistant2()) || ((VolleyBall)M).GetAssistant1().Equals(((VolleyBall)M).GetAssistant2()))
                                    {
                                        int z = 0, Ref, AS1, AS2;
                                        Random Rn = new Random();

                                        Ref = Rn.Next(0, Referees.Count);
                                        while (true)
                                        {
                                            AS1 = Rn.Next(0, Referees.Count);
                                            if (AS1 != Ref)
                                                break;
                                        }
                                        while (true)
                                        {
                                            AS2 = Rn.Next(0, Referees.Count);
                                            if (AS2 != AS1 && AS2 != Ref)
                                                break;
                                        }


                                        M.SetReferee(getReferees()[Ref]);
                                        ((VolleyBall)M).SetAssistant1(getReferees()[AS1]);
                                        ((VolleyBall)M).SetAssistant2(getReferees()[AS2]);

                                    }
                            }
                            if (M is DodgeBall && type == 'D' && M.getTeam1().getName() == name1 && M.getTeam2().getName() == name2)
                            {
                                M.setResult1(sc1);
                                M.setResult2(sc2);
                                if (WhatExistRef(mainname, mainsur))
                                    M.SetReferee(getReferees()[SearchRef(mainname, mainsur)]);
                                M.SetWhoWon();
                            }
                            else if (M is DodgeBall && type == 'D' && M.getTeam1().getName() == name2 && M.getTeam2().getName() == name1)
                            {
                                M.setResult1(sc2);
                                M.setResult2(sc1);
                                if (WhatExistRef(mainname, mainsur))
                                    M.SetReferee(getReferees()[SearchRef(mainname, mainsur)]);
                                M.SetWhoWon();
                            }
                            if (M is TugOfWar && type == 'T' && M.getTeam1().getName() == name1 && M.getTeam2().getName() == name2)
                            {
                                M.setResult1(sc1);
                                M.setResult2(sc2);
                                if (WhatExistRef(mainname, mainsur))
                                    M.SetReferee(getReferees()[SearchRef(mainname, mainsur)]);
                                M.SetWhoWon();
                            }
                            else if (M is TugOfWar && type == 'T' && M.getTeam1().getName() == name2 && M.getTeam2().getName() == name1)
                            {
                                M.setResult1(sc2);
                                M.setResult2(sc1);
                                if (WhatExistRef(mainname, mainsur))
                                    M.SetReferee(getReferees()[SearchRef(mainname, mainsur)]);
                                M.SetWhoWon();
                            }

                        }
                    }
                    Mat.Close();
                }
                else throw new FileException("Nie odnaleziono okreslonej ściezki\n", Path.GetFullPath(Ma));
            }
            catch (TourException ex)
            {
                MessageBox.Show("Nie znaleziono pliku z wynikami", "Error", MessageBoxButton.OK);

            }
            catch
            {
                MessageBox.Show("Nie znaleziono pliku z wynikami", "Error", MessageBoxButton.OK);
            }
        }//Przetestowac dzialnie dla final
         //Zapis do plików////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void Save()
        {
            StreamWriter Ref = new StreamWriter("Referees.txt");
            StreamWriter Tea = new StreamWriter("Teams.txt");
            foreach (Team T in Teams)
            {
                //Tea.WriteLine(T.getName() + " " + T.GetPlayers()[0].getName() + " " + T.GetPlayers()[0].getSurname() + " " + T.GetPlayers()[1].getName() + " " + T.GetPlayers()[1].getSurname() + " " + T.GetPlayers()[2].getName() + " " + T.GetPlayers()[2].getSurname() + " " + T.GetPlayers()[3].getName() + " " + T.GetPlayers()[3].getSurname() + ";");
                Tea.WriteLine(T.getName() + " " + T.GetPlayers()[0].ToString() + " " + T.GetPlayers()[1].ToString() + " " + T.GetPlayers()[2].ToString() + " " + T.GetPlayers()[3].ToString() + ";");
            }
            Tea.Close();
            foreach (Referee Re in Referees)
            {
                //Ref.WriteLine(Re.Name + " " + Re.Surname);
                Ref.WriteLine(Re.ToString() + " ");
            }
            Ref.Close();

        }///Ready
        public void SaveScore(Match M, bool Whatchange = false)
        {
            StreamWriter Sc;
            Sc = File.AppendText("Matches.txt");
            string Linia = "";
            if (M is VolleyBall)
                Linia = "V ";
            if (M is DodgeBall)
                Linia = "D ";
            if (M is TugOfWar)
                Linia = "T ";
            if (M is VolleyBall && M.WhatSemi)
                Linia = "S ";
            if (M is VolleyBall && M.WhatFinal)
                Linia = "F ";
            if (M.getResult1() > 0 || M.getResult2() > 0)
            {
                //Linia += M.getTeam1().getName() + " " + M.getTeam2().getName() + " " + M.ResulgetTeam1() + " " + M.ResulgetTeam2() + ";";
                Linia += M.ToString() + " " + M.GetReferee().ToString();
                if (M is VolleyBall)
                {
                    Linia += " " + ((VolleyBall)M).GetAssistant1().ToString() + " " + ((VolleyBall)M).GetAssistant2().ToString();
                }
                Linia += ";";
                Sc.WriteLine(Linia);
            }
            else if (Whatchange == true)
            {
                Linia += M.ToString() + " " + M.GetReferee().ToString();
                if (M is VolleyBall)
                {
                    Linia += " " + ((VolleyBall)M).GetAssistant1().ToString() + " " + ((VolleyBall)M).GetAssistant2().ToString();
                }
                Linia += ";";
                Sc.WriteLine(Linia);
            }

            Sc.Close();
        }//Przetestowac działanie dla final
         ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////




        //DOSTĘPY DO LIST///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Dostep do listy druzyn
        public List<Team> getTeams()
        {
            return Teams;
        }///Ready
        public void setTeams(List<Team> T)
        {
            Teams = T;
        }///Ready
            //Dostep do listy sedziow
        public List<Referee> getReferees()
        {
            return Referees;
        }///Ready
        public void setReferees(List<Referee> R)
        {
            Referees = R;
        }///Ready
            //Dostep do listy meczow
        public List<Match> getMatches()
        {
            return Matches;
        }///Ready
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }
}