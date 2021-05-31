using Kopakabana;
using People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matches
{
    public abstract class Match //Dodac metode eabstrakycjna equals bool i overide w klasasach dziedzicacych porownjaca nazwy  i dostepowe do team i ref
    {
        protected Team T1, T2;
        protected Team WhoWon;
        protected Referee REF;
        protected int Result1, Result2;
       
        public void AddWins(Team addwon)
        {
            addwon.Wins += 1;
        }
        public Team getTeam1()
        {
            return T1;
        }
        public Team getTeam2()
        {
            return T2;
        }
        public void setTeam1(Team name, Player p1, Player p2, Player p3);

        public override string ToString()
        {
            return T1.getName() + " - " + T2.getName();
        }
        public Team getWhoWon()
        {
            return WhoWon;
        }
        public abstract void SetWhoWon();
           
        public Referee GetReferee()
        {
            return REF;
        }

        public void SetReferee(Referee main)
        {
            REF = main;
        }

        public int getResult1()
        {
            return Result1;
        }
        public int getResult2()
        {
            return Result2;
        }
        public void setResult1(int score1)
        {
            Result1 = score1;
        }
        public void setResult2(int score2)
        {
            Result2 = score2;
        }
    }
}
