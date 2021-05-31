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
        private Match Semifinal1, Semifinal2, Final;
        public Tournament()
        {
            Referees = new List<Referee>();
            Teams = new List<Team>();
            Matches = new List<Match>();

        }///Ready
        public List<Team> getTop4()
        {
            Teams.Sort((x, y) => y.Wins.CompareTo(x.Wins));
            List<Team> best4 = new List<Team>(Teams.Take(4));
            return best4;
        }///Ready
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
                        foreach (Match M in Matches)
                        {

                            if (M is VolleyBall && type == 'V' && M.T1.Name == name1 && M.T2.Name == name2)
                            {
                                M.Result1 = sc1;
                                M.Result2 = sc2;
                                M.SetRefree(getReferees()[SearchRef(mainname, mainsur)]);
                                ((VolleyBall)M).SetAssistants(getReferees()[SearchRef(as1name, as1sur)], getReferees()[SearchRef(as2name, as2sur)]);
                                M.SetWhoWon();
                            }
                            else if (M is VolleyBall && type == 'V' && M.T1.Name == name2 && M.T2.Name == name1)
                            {
                                M.Result1 = sc2;
                                M.Result2 = sc1;
                                M.SetRefree(getReferees()[SearchRef(mainname, mainsur)]);
                                ((VolleyBall)M).SetAssistants(getReferees()[SearchRef(as1name, as1sur)], getReferees()[SearchRef(as2name, as2sur)]);
                                M.SetWhoWon();
                            }
                            if (M is DodgeBall && type == 'D' && M.T1.Name == name1 && M.T2.Name == name2)
                            {
                                M.Result1 = sc1;
                                M.Result2 = sc2;
                                M.SetRefree(getReferees()[SearchRef(mainname, mainsur)]);
                                M.SetWhoWon();
                            }
                            else if (M is DodgeBall && type == 'D' && M.T1.Name == name2 && M.T2.Name == name1)
                            {
                                M.Result1 = sc2;
                                M.Result2 = sc1;
                                M.SetRefree(getReferees()[SearchRef(mainname, mainsur)]);
                                M.SetWhoWon();
                            }
                            if (M is TugOfWar && type == 'T' && M.T1.Name == name1 && M.T2.Name == name2)
                            {
                                M.Result1 = sc1;
                                M.Result2 = sc2;
                                M.SetRefree(getReferees()[SearchRef(mainname, mainsur)]);
                                M.SetWhoWon();
                            }
                            else if (M is TugOfWar && type == 'T' && M.T1.Name == name2 && M.T2.Name == name1)
                            {
                                M.Result1 = sc2;
                                M.Result2 = sc1;
                                M.SetRefree(getReferees()[SearchRef(mainname, mainsur)]);
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

            }
            catch
            {
                MessageBox.Show("Nie znaleziono pliku z wynikami", "Error", MessageBoxButton.OK);
            }
        }//Dostep do skaldowych zamienc na metody dostepowe dodac czytanie sedziow
        //Zapis do plików////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void Save()
        {
            StreamWriter Ref = new StreamWriter("Referees.txt");
            StreamWriter Tea = new StreamWriter("Teams.txt");
            foreach (Team T in Teams)
            {
                Tea.WriteLine(T.Name + " " + T.P1.getName() + " " + T.P1.getSurname() + " " + T.P2.getName() + " " + T.P2.getSurname() + " " + T.P3.getName() + " " + T.P3.getSurname() + " " + T.P4.getName() + " " + T.P4.getSurname() + ";");
            }
            Tea.Close();
            foreach (Referee Re in Referees)
            {
                //Ref.WriteLine(Re.Name + " " + Re.Surname);
                Ref.WriteLine(Re.ToString() + " ");
            }
            Ref.Close();

        }//Zapis druzyn za pomoca ToString()
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
            if (M.Result1 > 0 || M.Result2 > 0)
            {
                //Linia += M.T1.getName() + " " + M.T2.getName() + " " + M.Result1 + " " + M.Result2 + ";";
                Linia += M.ToString() + " " + M.GetReferee().ToString();
                if (M is VolleyBall)
                {
                    Linia += " " + ((VolleyBall)M).AS1.ToString() + " " + ((VolleyBall)M).AS2.ToString();
                }
                Linia += ";";
                Sc.WriteLine(Linia);
            }
            else if (Whatchange == true)
            {
                Linia += M.ToString() + " " + M.GetReferee().ToString();
                if (M is VolleyBall)
                {
                    Linia += " " + ((VolleyBall)M).AS1.ToString() + " " + ((VolleyBall)M).AS2.ToString();
                }
                Linia += ";";
                Sc.WriteLine(Linia);
            }

            Sc.Close();
        }//Dodac zapis sedziow
        //Generowanie meczow ora ustalnie ilosci wygranych
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
                ReadScore();
            }
            catch
            {

            }
        }///Ready
        public void CountWins()
        {
            //Ustawianie wygranych na zero aby przy ponowym liczeniu liczylo od zera
            foreach (Team T in Teams)
            {
                T.Wins = 0;
            }
            //Liczenie wygranych 
            foreach (Match M in Matches)
            {
                foreach (Team T in Teams)
                {
                    if (M.getWhoWon() != null)
                        if (M.getWhoWon().getName() == T.getName())
                        {
                            T.Wins++;
                        }
                }
            }
        }///Ready
        //Metody zarzadzania//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Druzynami
        public void ChangeName(string pop, string nast)
        {
            foreach (Match M in Matches)
            {
                if (M.T1.getName() == pop)
                {
                    M.T1.setName(nast);
                }
                if (M.T2.getName() == pop)
                {
                    M.T2.setName(nast);
                }
            }
        }///Ready
        public void SearchName(string akt)
        {
            foreach (Match M in Matches)
            {
                if (M.T1.getName() == akt)
                {
                    SaveScore(M);
                }
                if (M.T2.getName() == akt)
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
        }
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
            throw new TourException();
        }//.Zastapic equals
        public void CheckRef(string name, string surname)
        {
            foreach (Referee R in Referees)
            {
                if (R.getName() == name && R.getSurname() == surname)
                {
                    throw new ExistNameException(" jest zarejetorwanym sedzią", name, surname);
                }
            }
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///Gettery i settery/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Ustawianie list////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Ustawia dane druzyny po edycji
        public void setTeams(int index, string name, Player p1, Player p2, Player p3, Player p4)
        {
            Teams[index] = new Team(name, p1, p2, p3, p4);
        }///Ready
        public void setReferees(int index, string name, string surname)
        {
            Referees[index] = new Referee(name, surname);
        }
        public void UpdateMatch(string popname,string popsur,string name, string surname,int index)
        {
            foreach (Match M in Matches)
            {
                if (M.GetReferee().Equals(new Referee(popname, popsur)))
                {
                    M.SetReferee(Referees[index]);
                }
                if (M is VolleyBall)
                { 
                    if(((VolleyBall)M).GetAssistant1().Equals(new Referee(popname, popsur)))
                    {
                        M.SetReferee(Referees[index]);
                    }
                    if (((VolleyBall)M).GetAssistant2().Equals(new Referee(popname, popsur)))
                    {
                        M.SetReferee(Referees[index]);
                    } 
                }
            }
        }
        //Kopjuja dane do list głownych
        public void setTeams(List<Team> T)
        {
            Teams = T;
        }///Ready
        public void setReferees(List<Referee> R)
        {
            Referees = R;
        }///Ready
            //Ustawiawie wynikow w liscie metchow
        public void setMatch(int index, int res1, int res2)
        {
            var M = Matches[index];
            Matches[index].Result1 = res1;
            Matches[index].Result2 = res2;
            /*if (M is VolleyBall)
            {
                ((VolleyBall)Matches[index]).Result1 = res1;
                ((VolleyBall)Matches[index]).Result2 = res2;
            }*/

        }//Zmienic na metode dotepowa do result
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Zwraca liste druzyn////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public List<Team> getTeams()
        {
            return Teams;
        }///Ready
        //Zwraca liste sedziow
        public List<Referee> getReferees()
        {
            return Referees;
        }///Ready
        //Zwraca liste maeczow
        public List<Match> getMatches()
        {
            return Matches;
        }///Ready
    }
}

