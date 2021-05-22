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
        public Referee Ref;
        public override string ToString()
        {
            return T1.getName() + " - " + T2.getName();
        }
        public void AddWins(Team addwon)
        {
            addwon.Wins += 1;
        }

        public void SetRefree(Referee main)
        {
            Ref = main;
        }

        public Team getWhoWon()
        {
            return WhoWon;
        }

        public abstract void SetWhoWon(bool t1);

    }
}
