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
    class Tournament
    {
        private List<Referee> Referees;
        private List<Team> Teams;
        private List<Match> Matches;
        public Tournament()
        {
            Referees = new List<Referee>();
            Teams = new List<Team>();
            Matches = new List<Match>();
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
                        for (y = x + 1; linia[y] != ' '; y++)
                        {
                            nazwisko += linia[y];
                        }
                        PL.Add(new Player(imie, nazwisko));
                        imie = "";
                        nazwisko = "";
                        i = y;
                    }
                    Teams.Add(new Team(nazwa,PL[0], PL[1], PL[2], PL[3]));
                    nazwa = "";
                }
            }
            catch
            {
                MessageBox.Show("Nie znaleziono pliku", "Error", MessageBoxButton.OK);
            }
            int q = 0;
            foreach (Team T in Teams)
            {
                for(int i=q+1;i<Teams.Count;i++)
                {
                    Matches.Add(new VolleyBall(T, Teams[i]));
                }
                q++;
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
    }
}

