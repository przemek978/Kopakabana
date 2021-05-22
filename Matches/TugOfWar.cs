using People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matches
{
    class TugOfWar:Match
    {
        public TugOfWar(Team t1, Team t2)
        {
            T1 = t1;
            T2 = t2;
        }

        //public bool Equals(TugOfWar T) { }
        public override void SetWhoWon(bool t1)
        {
            if (t1 == true)
            {
                WhoWon = T1;
                AddWins(T1);
            }
            else
            {
                WhoWon = T2;
                AddWins(T2);
            }
        }

    }

}
