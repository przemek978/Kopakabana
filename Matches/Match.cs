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
       private Team T1, T2;
       private Team WhoWon;
       Referee Ref;
        public override string ToString()
        {
            return T1.getName() + " - " + T2.getName();
        }

        public abstract bool Equals();

        public void AddWins()
        {
            
        }

        public Team getWhoWon()
        {
            return WhoWon;
        }

        public  void setWhoWon() 
        {
            
        }
    }
}
