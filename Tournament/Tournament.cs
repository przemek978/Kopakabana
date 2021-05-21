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
        public void Read()
        {
            try
            {

                string linia, imie = "", nazwisko = "";
                int i = 0, j, x, y, z;
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
            catch
            {
                MessageBox.Show("Nie znaleziono pliku", "Error", MessageBoxButton.OK);
            }

        }
        public void Save()
        {
            StreamWriter Ref = new StreamWriter("Referees.txt");
            StreamWriter Tea = new StreamWriter("Teams.txt");
            foreach (Team T in Teams)
            {
                Tea.WriteLine(T.Name + " " + T.P1.Name + " " + T.P1.Surname + " " + T.P2.Name + " " + T.P2.Surname + " " + T.P3.Name + " " + T.P3.Surname + " " + T.P4.Name + " " + T.P4.Surname + ";");
            }
            Tea.Close();
            foreach (Referee Re in Referees)
            {
                Ref.WriteLine(Re.Name + " " + Re.Surname);
            }
            Ref.Close();

        }
        public void GenerateMatches()
        {
            int q = 0;
            foreach (Team T in Teams)
            {
                for (int i = q + 1; i < Teams.Count; i++)
                {
                    Matches.Add(new VolleyBall(T, Teams[i]));
                }
                q++;
            }
        }
        public void ReadScore()
        {
            try
            {
                StreamReader Mat = new StreamReader("Matches.txt");
                int i, j, z, t, sc1 = 0, sc2 = 0;
                char type;
                string linia, name1, name2, score1, score2;
                while ((linia = Mat.ReadLine()) != null)
                {
                    type = linia[0];
                    name1 = ""; name2 = ""; score1 = ""; score2 = "";
                    if (type == 'V')
                    {
                        for (i = 2; linia[i] != ' '; i++)
                        {
                            name1 += linia[i];
                        }
                        for (j = i + 1; linia[j] != ' '; j++)
                        {
                            name2 += linia[j];

                        }
                        for (z = j + 1; linia[z] != ' '; z++)
                        {
                            score1 += linia[z];
                        }
                        for (t = z + 1; linia[t] != ';'; t++)
                        {
                            score2 += linia[t];

                        }
                        sc1 = int.Parse(score1);
                        sc2 = int.Parse(score2);
                    }
                    foreach (Match M in Matches)
                    {
                        if (M is VolleyBall && M.T1.Name == name1 && M.T2.Name == name2)
                        {
                            ((VolleyBall)M).Result1 = sc1;
                            ((VolleyBall)M).Result2 = sc2;
                            M.setWhoWon(true);
                        }
                        if (!(M is VolleyBall) && M.T1.Name == name1 && M.T2.Name == name2)
                        {
                            if (sc1 == 1)
                            {
                                M.setWhoWon(true);
                            }
                            if (sc2 == 1)
                            {
                                M.setWhoWon(false);
                            }


                        }
                    }

                }
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
            if (M is VolleyBall)
            {
                ((VolleyBall)Matches[index]).Result1 = res1;
                ((VolleyBall)Matches[index]).Result2 = res2;
            }

        }
    }
}

