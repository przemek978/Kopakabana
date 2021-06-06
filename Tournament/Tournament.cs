using Matches;
using People;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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

        public List<Team> GetTop4()
        {
            Teams.Sort((x, y) => y.GetWins().CompareTo(x.GetWins()));
            List<Team> best4 = new List<Team>(Teams.Take(4));
            return best4;
        }///Ready
        public List<Referee> Get3Referees()
        {
            int Ref, AS1, AS2;
            Random Rn = new Random();
            Thread.Sleep(20);
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
        public void AddReferee(Referee R)
        {
            Referees.Add(R);
        }
        public void AddTeam(Team T)
        {
            Teams.Add(T);
        }

        //GENEROWANIE//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //Meczów każdy z każdym
        public void GenerateMatches()
        {
            try
            {
                if (Referees.Count < 3)
                    throw new Exception();
                int q = 0, Ref;
                Matches = new List<Match>();
                Random Rn = new Random();
                ///Generowanie dla siatkowki
                foreach (Team T in Teams)
                {
                    for (int i = q + 1; i < Teams.Count;)
                    {
                       /* Ref = Rn.Next(0, Referees.Count);
                        AS1 = Rn.Next(0, Referees.Count);
                        AS2 = Rn.Next(0, Referees.Count);
                        if (Ref != AS1 && Ref != AS2 && AS1 != AS2)
                        {
                            Matches.Add(new VolleyBall(T, Teams[i], Referees[Ref], Referees[AS1], Referees[AS2]));
                            i++;
                        }*/
                        var REF = Get3Referees();
                        Matches.Add(new VolleyBall(T, Teams[i], REF[0], REF[1], REF[2]));
                        i++;
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
                GenerateFinal();
                ReadScore();
            }
            catch
            {

            }
        }///Ready
            //Meczów półfinałowych
        public void GenerateSemifinals()
        {
            ReadScore();
            if (WhatAll())
            {
                CountWins();
                bool whatis = false;
                var top4 = GetTop4();
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
        }
            //Finału
        public void GenerateFinal()
        {
            try
            {
                ReadScore();
                if (WhatAll())
                {
                    bool whatis = false;
                    int i = 0;
                    if (Semifinal1 == null || Semifinal2 == null)
                        throw new Exception();
                    if (Semifinal1.GetWhoWon() != null && Semifinal2.GetWhoWon() != null)
                    {
                        var RefThree = Get3Referees();
                        Final = new VolleyBall(Semifinal1.GetWhoWon(), Semifinal2.GetWhoWon(), RefThree[0], RefThree[1], RefThree[2]) { WhatFinal = true };
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

            }
            catch { }
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        //ZARZĄDZANIE//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //Druzynami
        public void CheckName(string name)
        {
            foreach (Team T in Teams)
            {
                if (T.GetName() == name)
                    throw new ExistNameException(" juz istnieje", name);
            }
        }///Ready
        public void SearchName(string akt)
        {
            foreach (Match M in Matches)
            {
                if (M.GetTeam1().GetName() == akt)
                {
                    SaveScore(M);
                }
                if (M.GetTeam2().GetName() == akt)
                {
                    SaveScore(M);
                }
            }
        }///Ready
            //Sedziami
        public void CheckRef(string name, string surname)
        {
            foreach (Referee R in Referees)
            {
                if (R.GetName() == name && R.GetSurname() == surname)
                {
                    throw new ExistNameException(" jest zarejetorwanym sedzią", name, surname);
                }
            }
        }///Ready
        public int SearchRef(string name, string surname)
        {
            foreach (Referee R in Referees)
            {
                /*if (R.GetName() == name && R.GetSurname() == surname)
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
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///

        //OBLICZNIE WYGRANYCH//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void CountWins()
        {
            //Ustawianie wygranych na zero aby przy ponowym liczeniu liczylo od zera
            foreach (Team T in Teams)
            {
                T.SetWins(0);
            }
            //Liczenie wygranych 
            foreach (Match M in Matches)
            {
                foreach (Team T in Teams)
                {
                    if (M.GetWhoWon() != null)
                        if (M.GetWhoWon().Equals(T) && M.WhatSemi == false && M.WhatFinal == false)
                        {
                            T.SetWins(T.GetWins() + 1);
                        }
                }
            }
        }///Ready
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        

        //Testy//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
        public bool WhatAll()
        {
            foreach (Match M in Matches)
            {
                if (M.GetWhoWon() == null)
                    return false;
            }
            return true;
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///GetTERY I SetTERY/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //USTAWIANIE LIST////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //Edycja sedziow
        public void SetReferee(int index, string name, string surname)
        {
            // Referees[index] = new Referee(name, surname);
            Referees[index].SetName(name);
            Referees[index].SetSurname(surname);
        }
            //Edycja Druzyn
        public void SetTeam(int index, string name, Player p1, Player p2, Player p3, Player p4)
        {
            Teams[index].SetPlayers(p1, p2, p3, p4);
            Teams[index].SetName(name);
        }///Ready
            //Edycja Wyników
        public void SetResult(int index, int res1, int res2)
        {
            Matches[index].SetResult1(res1);
            Matches[index].SetResult2(res2);
        }///Ready
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        
        //DOSTĘPY DO LIST///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //Dostep do listy druzyn
        public List<Team> GetTeams()
        {
            return Teams;
        }///Ready
        public void SetTeams(List<Team> T)
        {
            Teams = T;
        }///Ready
            //Dostep do listy sedziow
        public List<Referee> GetReferees()
        {
            return Referees;
        }///Ready
        public void SetReferees(List<Referee> R)
        {
            Referees = R;
        }///Ready
            //Dostep do listy meczow
        public List<Match> GetMatches()
        {
            return Matches;
        }///Ready
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

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

                MessageBox.Show(ex.Message + ex.GetName(), "Error", MessageBoxButton.OK);
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
                        var Mecze = GetMatches();
                        foreach (Match M in Matches)
                        {
                            if (((M is VolleyBall && type == 'V' && M.WhatSemi == false && M.WhatFinal == false) || (M is VolleyBall && type == 'S' && M.WhatSemi) || (M is VolleyBall && type == 'F' && ((VolleyBall)M).WhatFinal)) && ((M.GetTeam1().GetName() == name1) && (M.GetTeam2().GetName() == name2)))
                            {
                                M.SetResult1(sc1);
                                M.SetResult2(sc2);
                                if (WhatExistRef(mainname, mainsur))
                                    M.SetReferee(GetReferees()[SearchRef(mainname, mainsur)]);
                                if (WhatExistRef(as1name, as1sur))
                                    ((VolleyBall)M).SetAssistant1(GetReferees()[SearchRef(as1name, as1sur)]);
                                if (WhatExistRef(as2name, as2sur))
                                    ((VolleyBall)M).SetAssistant2(GetReferees()[SearchRef(as2name, as2sur)]);
                                M.SetWhoWon();
                                if (M.GetReferee().Equals(((VolleyBall)M).GetAssistant1()) || M.GetReferee().Equals(((VolleyBall)M).GetAssistant2()) || ((VolleyBall)M).GetAssistant1().Equals(((VolleyBall)M).GetAssistant2()))
                                {
                                    int  Ref, AS1, AS2;
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

                                    M.SetReferee(GetReferees()[Ref]);
                                    ((VolleyBall)M).SetAssistant1(GetReferees()[AS1]);
                                    ((VolleyBall)M).SetAssistant2(GetReferees()[AS2]);

                                }
                            }
                            else if (((M is VolleyBall && type == 'V' && M.WhatSemi == false && M.WhatFinal == false) || (M is VolleyBall && type == 'S' && ((VolleyBall)M).WhatSemi) || (M is VolleyBall && type == 'F' && ((VolleyBall)M).WhatFinal)) && ((M.GetTeam1().GetName() == name2) && (M.GetTeam2().GetName() == name1)))
                            {
                                M.SetResult1(sc2);
                                M.SetResult2(sc1);
                                if (WhatExistRef(mainname, mainsur))
                                    M.SetReferee(GetReferees()[SearchRef(mainname, mainsur)]);
                                if (WhatExistRef(as1name, as1sur))
                                    ((VolleyBall)M).SetAssistant1(GetReferees()[SearchRef(as1name, as1sur)]);
                                if (WhatExistRef(as2name, as2sur))
                                    ((VolleyBall)M).SetAssistant2(GetReferees()[SearchRef(as2name, as2sur)]);
                                M.SetWhoWon();
                                if (M.GetReferee().Equals(((VolleyBall)M).GetAssistant1()) || M.GetReferee().Equals(((VolleyBall)M).GetAssistant2()) || ((VolleyBall)M).GetAssistant1().Equals(((VolleyBall)M).GetAssistant2()))
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


                                    M.SetReferee(GetReferees()[Ref]);
                                    ((VolleyBall)M).SetAssistant1(GetReferees()[AS1]);
                                    ((VolleyBall)M).SetAssistant2(GetReferees()[AS2]);

                                }
                            }
                            if (M is DodgeBall && type == 'D' && M.GetTeam1().GetName() == name1 && M.GetTeam2().GetName() == name2)
                            {
                                M.SetResult1(sc1);
                                M.SetResult2(sc2);
                                if (WhatExistRef(mainname, mainsur))
                                    M.SetReferee(GetReferees()[SearchRef(mainname, mainsur)]);
                                M.SetWhoWon();
                            }
                            else if (M is DodgeBall && type == 'D' && M.GetTeam1().GetName() == name2 && M.GetTeam2().GetName() == name1)
                            {
                                M.SetResult1(sc2);
                                M.SetResult2(sc1);
                                if (WhatExistRef(mainname, mainsur))
                                    M.SetReferee(GetReferees()[SearchRef(mainname, mainsur)]);
                                M.SetWhoWon();
                            }
                            if (M is TugOfWar && type == 'T' && M.GetTeam1().GetName() == name1 && M.GetTeam2().GetName() == name2)
                            {
                                M.SetResult1(sc1);
                                M.SetResult2(sc2);
                                if (WhatExistRef(mainname, mainsur))
                                    M.SetReferee(GetReferees()[SearchRef(mainname, mainsur)]);
                                M.SetWhoWon();
                            }
                            else if (M is TugOfWar && type == 'T' && M.GetTeam1().GetName() == name2 && M.GetTeam2().GetName() == name1)
                            {
                                M.SetResult1(sc2);
                                M.SetResult2(sc1);
                                if (WhatExistRef(mainname, mainsur))
                                    M.SetReferee(GetReferees()[SearchRef(mainname, mainsur)]);
                                M.SetWhoWon();
                            }

                        }
                    }
                    Mat.Close();
                }
                else throw new FileException("Nie odnaleziono okreslonej ściezki\n", Path.GetFullPath(Ma));
            }
            catch (FileException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK);

            }
            catch
            {
                MessageBox.Show("Nie znaleziono pliku z wynikami", "Error", MessageBoxButton.OK);
            }
        }///Ready
            //Zapis do plików////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void Save()
        {
            StreamWriter Ref = new StreamWriter("Referees.txt");
            StreamWriter Tea = new StreamWriter("Teams.txt");
            foreach (Team T in Teams)
            {
                //Tea.WriteLine(T.GetName() + " " + T.GetPlayers()[0].GetName() + " " + T.GetPlayers()[0].GetSurname() + " " + T.GetPlayers()[1].GetName() + " " + T.GetPlayers()[1].GetSurname() + " " + T.GetPlayers()[2].GetName() + " " + T.GetPlayers()[2].GetSurname() + " " + T.GetPlayers()[3].GetName() + " " + T.GetPlayers()[3].GetSurname() + ";");
                Tea.WriteLine(T.GetName() + " " + T.GetPlayers()[0].ToString() + " " + T.GetPlayers()[1].ToString() + " " + T.GetPlayers()[2].ToString() + " " + T.GetPlayers()[3].ToString() + ";");
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
            if (M.GetResult1() > 0 || M.GetResult2() > 0)
            {
                //Linia += M.GetTeam1().GetName() + " " + M.GetTeam2().GetName() + " " + M.ResulGetTeam1() + " " + M.ResulGetTeam2() + ";";
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
        }///Ready
         ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    }
}