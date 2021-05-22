using People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace People
{
    public class Team
    {
        public string Name;
        public Player P1, P2, P3, P4;
        public int Wins;
        public Team(string name,Player p1, Player p2, Player p3, Player p4)
        {
            Name = name;
            Wins = 0;
            P1 = p1;
            P2 = p2;
            P3 = p3;
            P4 = p4;
        }
        public string getName()
        {
            return Name;
        }
        public int getWins(Team t)
        {
            return t.Wins;
        }

        public void setWins(int wins)
        {
            this.Wins = wins;
        }
        public override string ToString()
        {
            return Name + " " + Wins;
        }

        /*public override string ToString()
        {
            return Name+"\n"+P1.ToString() + "\n"+ P2.ToString() + "\n"+ P3.ToString() + "\n"+ P4.ToString() ;
        }*/
    }
}
