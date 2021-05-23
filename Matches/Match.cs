using Kopakabana;
using People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matches
{
    public abstract class Match
    {
        public Team T1, T2;
        protected Team WhoWon;
        public Referee REF;
        public int Result1, Result2;
        public abstract void SetWhoWon();
       
        public void AddWins(Team addwon)
        {
            addwon.Wins += 1;
        }

        public void SetRefree(Referee main)
        {
            REF = main;
        }

        public Team getWhoWon()
        {
            return WhoWon;
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
        public override string ToString()
        {
            return T1.getName() + " - " + T2.getName();
        }
    }
}
