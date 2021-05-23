using People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matches
{
    class DodgeBall : Match
    {
        public DodgeBall(Team t1, Team t2,Referee Ref)
        {
            T1 = t1;
            T2 = t2;
            REF = Ref;
        }

        //public bool Equals(DodgeBall D) { }
        public override void SetWhoWon()
        {
            if (Result1 > Result2 && Result1 == 1)
            {
                WhoWon = T1;
                AddWins(T1);
            }
            else if (Result1 < Result2 && Result2 == 1)
            {
                WhoWon = T2;
                AddWins(T2);
            }
            else
                WhoWon = null;
        }
        public override string ToString()
        {
            //return T1.getName() + " - " + T2.getName() + "\t\t\t\t\t\t" + getResult1() + " : " + getResult2();
            return T1.getName() + " - " + T2.getName() + " " + getResult1() + " : " + getResult2();
        }
    }
}
