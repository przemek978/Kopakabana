using People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace People
{
    public class Team //metody dotepowe do player a dokladnie ich składowych (returnb list 4 elementowa)
    {
        private string Name;
        private Player P1, P2, P3, P4;
        private int Wins;
        List<Player> Gracze;

        public Team(string name,Player p1, Player p2, Player p3, Player p4)
        {
            Name = name;
            Wins = 0;
            P1 = p1;
            P2 = p2;
            P3 = p3;
            P4 = p4;            
        }
        public bool Equals(Team T)
        {
            if (T.GetName() == GetName() && T.GetName() == GetName())
                return true;
            else
                return false;
        }
        //Dostep do zawodnikow
        public void SetPlayers(Player p1, Player p2, Player p3, Player p4)
        {
            P1 = p1;
            P2 = p2;
            P3 = p3;
            P4 = p4;
        }

        public List<Player> GetPlayers()
        {
            Gracze = new List<Player>();
            Gracze.Add(P1);
            Gracze.Add(P2);
            Gracze.Add(P3);
            Gracze.Add(P4);
            return Gracze;
        }
        //Dostep do nazwy
        public string GetName()
        {
            return Name;
        }
        public void SetName(string name)
        {
            Name = name;
        }
        //Dostep do liczby wygranych
        public int GetWins()
        {
            return this.Wins;
        }
        public void SetWins(int wins)
        {
            this.Wins = wins;
        }
        /*public void SetWins()
        {
            Wins++;
        }*/
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public override string ToString()
        {
            return Name + " " + Wins;
        }

    }
}
