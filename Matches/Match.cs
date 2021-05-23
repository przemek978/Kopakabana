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
        public Team T1, T2;
        protected Team WhoWon;
        public Referee REF;
        public int Result1, Result2;
       
        public void AddWins(Team addwon)
        {
            addwon.Wins += 1;
        }

        public override string ToString()
        {
            return T1.getName() + " - " + T2.getName();
        }
        public Team getWhoWon()
        {
            return WhoWon;
        }
        public abstract void SetWhoWon();

        public void SetRefree(Referee main)
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
