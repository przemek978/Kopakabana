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
        public List<Team> Teams { get; set; }
        private List<Match> Matches { get; set; }
        private Match Semifinal1, Semifinal2, Final;
        public Tournament()
        {
            Referees = new List<Referee>();
            Teams = new List<Team>();
            Matches = new List<Match>();

        }
        public List<Team> getTop4()
        {
            Teams.Sort((x, y) => x.Wins.CompareTo(y.Wins));
            List<Team> best4 = new List<Team>(Teams.Take(4));
            return best4;
        }
        public void Read()
        {
            try
            {
                string linia, imie = "", nazwisko = "";
                int i = 0, j, x, y, z, q, Id;
                StreamReader Ref = new StreamReader("Referees.txt");
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
                StreamReader T = new StreamReader("Teams.txt");
                string nazwa = "", id = "";
                while ((linia = T.ReadLine()) != null)
                {
                    List<Player> PL = new List<Player>();
                    for (i = 0; linia[i] != ' '; i++)
                    {
                        nazwa += linia[i];
                    }
                    /*for (q = i + 1; linia[q] != ' '; q++)
                    {
                        id += linia[q];
                    }*/
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
                    //Id = int.Parse(id);
                    Teams.Add(new Team(nazwa, PL[0], PL[1], PL[2], PL[3]));
                    nazwa = "";

                }
                T.Close();
            }
            catch
            {
                MessageBox.Show("Nie znaleziono pliku", "Error", MessageBoxButton.OK);
            }

        }
        public void Save()
        {
            StreamWriter Ref = new StreamWriter("Referees.txt");
            StreamWriter Tea = new StreamWriter("Teams.txt");
            // StreamWriter Sc = new StreamWriter("Matches.txt");
            foreach (Team T in Teams)
            {
                Tea.WriteLine(T.Name + " " + T.P1.Name + " " + T.P1.Surname + " " + T.P2.Name + " " + T.P2.Surname + " " + T.P3.Name + " " + T.P3.Surname + " " + T.P4.Name + " " + T.P4.Surname + ";");
            }
            Tea.Close();
            foreach (Referee Re in Referees)
            {
                //Ref.WriteLine(Re.Name + " " + Re.Surname);
                Ref.WriteLine(Re.ToString());
            }
            Ref.Close();

        }
        public void GenerateMatches()
        {
            int q = 0, Ref, AS1, AS2;
            Matches = new List<Match>();
            Random Rn = new Random();
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
            q = 0;
            foreach (Team T in Teams)
            {
                for (int i = q + 1; i < Teams.Count; i++)
                {
                    Ref = Rn.Next(0, Referees.Count);
                    Matches.Add(new TugOfWar(T, Teams[i], Referees[Ref]));
                }
                q++;
            }
            q = 0;
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
        public void SaveScore(Match M,bool Whatchange=false)
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
                Linia += M.ToString() + ";";
                Sc.WriteLine(Linia);
            }
            else if(Whatchange==true)
            {
                Linia += M.ToString() + ";";
                Sc.WriteLine(Linia);
            }
            /*foreach (Match M in Matches)
            {
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
                    Linia += M.ToString() + ";";
                    Sc.WriteLine(Linia);
                }
            }*/
            Sc.Close();
        }
                /*if(M is VolleyBall && ((VolleyBall)M).Result1 > 0 || ((VolleyBall)M).Result2 > 0)
                    Sc.WriteLine("V "+M.T1.Name+" "+M.T2.Name+" "+((VolleyBall)M).Result1+" "+ ((VolleyBall)M).Result2+";");*/
                /*if (M is VolleyBall && ((VolleyBall)M).Result1 > 0 || ((VolleyBall)M).Result2 > 0)
                    Sc.WriteLine("V " + M.T1.getName() + " " + M.T2.getName() + " " + ((VolleyBall)M).Result1 + " " + ((VolleyBall)M).Result2 + ";");
                if (M is DodgeBall && M.Result1 > 0 || M.Result2 > 0)
                    Sc.WriteLine("D " + M.T1.getName() + " " + M.T2.getName() + " " + ((DodgeBall)M).Result1 + " " + ((DodgeBall)M).Result2 + ";");
                if (M is TugOfWar && M.Result1 > 0 || M.Result2 > 0)
                    Sc.WriteLine("T " + M.T1.getName() + " " + M.T2.getName() + " " + ((TugOfWar)M).Result1 + " " + ((TugOfWar)M).Result2 + ";");*/
        public void ReadScore()
        {
            try
            {
                StreamReader Mat = new StreamReader("Matches.txt");
                int i, j, z, t,q,g, sc1 = 0, sc2 = 0, Id1 = 0, Id2 = 0;
                char type;
                string linia, name1, name2, score1, score2;
                /*while ((linia = Mat.ReadLine()) != null)
                {
                    type = linia[0];
                    nazwa1 = ""; nazwa2 = ""; score1 = ""; score2 = "";
                    if (type == 'V')
                    {
                        for (i = 2; linia[i] != ' '; i++)
                        {
                            nazwa1 += linia[i];
                        }
                        for (j = i + 1; linia[j] != ' '; j++)
                        {
                            nazwa2 += linia[j];

                        }
                        for (z = j + 1; linia[z] != ' '; z++)
                        {
                            score1 += linia[z];
                        }
                        for (t = z + 1; linia[t] != ';'; t++)
                        {
                            score2 += linia[t];

                        }
                        //Id1 = int.Parse(id1);
                        //Id2 = int.Parse(id2);
                        sc1 = int.Parse(score1);
                        sc2 = int.Parse(score2);
                    }

                    foreach (Match M in Matches)
                    {
                        if (M is VolleyBall && M.T1.ID == Id1 && M.T2.ID == Id2)
                        {
                            ((VolleyBall)M).Result1 = sc1;
                            ((VolleyBall)M).Result2 = sc2;
                            M.SetWhoWon(true);
                        }
                        else if (M.T1.ID == Id1 && M.T2.ID == Id2)
                        {
                            ((VolleyBall)M).Result1 = sc2;
                            ((VolleyBall)M).Result2 = sc1;
                            M.SetWhoWon(true);
                        }
                        if (!(M is VolleyBall) && M.T1.ID == Id1 && M.T2.ID == Id2 || M.T1.ID == Id1 && M.T2.ID == Id2)
                        {
                            if (sc1 == 1)
                            {
                                M.SetWhoWon(true);
                            }
                            if (sc2 == 1)
                            {
                                M.SetWhoWon(false);
                            }


                        }
                    }
                }*/


                while ((linia = Mat.ReadLine()) != null)
                {
                    type = linia[0];
                    name1 = ""; name2 = ""; score1 = ""; score2 = "";

                    for (i = 2; linia[i] != ' '; i++)
                    {
                        name1 += linia[i];
                    }
                    for (q = i + 1; linia[q] != '-'; q++)
                    {
                        

                    }
                    for (j = q + 2; linia[j] != ' '; j++)
                    {
                        name2 += linia[j];

                    }
                    for (z = j + 1; linia[z] != ' '; z++)
                    {
                        score1 += linia[z];
                    }
                    for (g = z + 1; linia[g] != ':'; g++)
                    {


                    }
                    for (t = g + 2; linia[t] != ';'; t++)
                    {
                        score2 += linia[t];

                    }
                    sc1 = int.Parse(score1);
                    sc2 = int.Parse(score2);


                    foreach (Match M in Matches)
                    {
                        if (M is VolleyBall && type=='V' && M.T1.Name == name1 && M.T2.Name == name2)
                        {
                            M.Result1 = sc1;
                            M.Result2 = sc2;
                            M.SetWhoWon();
                        }
                        else if (M is VolleyBall && type == 'V' && M.T1.Name == name2 && M.T2.Name == name1)
                        {
                            M.Result1 = sc2;
                            M.Result2 = sc1;
                            M.SetWhoWon();
                        }
                        if (M is DodgeBall && type == 'D' && M.T1.Name == name1 && M.T2.Name == name2)
                        {
                            M.Result1 = sc1;
                            M.Result2 = sc2;
                            M.SetWhoWon();
                        }
                        else if (M is DodgeBall && type == 'D' && M.T1.Name == name2 && M.T2.Name == name1)
                        {
                            M.Result1 = sc2;
                            M.Result2 = sc1;
                            M.SetWhoWon();
                        }
                        if (M is TugOfWar && type == 'T' && M.T1.Name == name1 && M.T2.Name == name2)
                        {
                            M.Result1 = sc1;
                            M.Result2 = sc2;
                            M.SetWhoWon();
                        }
                        else if (M is TugOfWar && type == 'T' && M.T1.Name == name2 && M.T2.Name == name1)
                        {
                            M.Result1 = sc2;
                            M.Result2 = sc1;
                            M.SetWhoWon();
                        }
                        /* if (M is VolleyBall && M.T1.Name == name1 && M.T2.Name == name2)
                         {
                             M.Result1 = sc1;
                             M.Result2 = sc2;
                             M.SetWhoWon(true);
                         }
                         else if (M.T1.Name == name2 && M.T2.Name == name1)
                         {
                             M.Result1 = sc2;
                             M.Result2 = sc1;
                             M.SetWhoWon(true);
                         }
                         if (!(M is VolleyBall) && M.T1.Name == name1 && M.T2.Name == name2 || M.T1.Name == name2 && M.T2.Name == name1)
                         {
                             if (sc1 == 1)
                             {
                                 M.SetWhoWon(true);
                             }
                             if (sc2 == 1)
                             {
                                 M.SetWhoWon(false);
                             }


                         }*/
                    }

                }
                Mat.Close();
            }
            catch
            {
                MessageBox.Show("Nie znaleziono pliku z wynikami", "Error", MessageBoxButton.OK);
            }
        }
        public void CountWins()
        {
            foreach (Team T in Teams)
            {
                T.Wins = 0;
            }
            foreach (Match M in Matches)
            {
                foreach (Team T in Teams)
                {
                    if (M.getWhoWon() != null)
                        if (M.getWhoWon().Name == T.Name)
                        {
                            T.Wins++;
                        }
                }
            }
        }
        public List<Team> getTeams()
        {
            return Teams;
        }
        public List<Referee> getReferees()
        {
            return Referees;
        }
        public List<Match> getMatches()
        {
            return Matches;
        }
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

        }
        public void setTeams(int index, string name, Player p1, Player p2, Player p3, Player p4)
        {
            Teams[index] = new Team(name, p1, p2, p3, p4);
        }
        public void setTeams(List<Team> T)
        {
            Teams = T;
        }
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
        }
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
        }
    }
}

