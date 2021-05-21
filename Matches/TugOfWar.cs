using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matches
{
    class TugOfWar:Match
    {
        public TugOfWar() { }

        //public bool Equals(TugOfWar T) { }
        public override void setWhoWon(bool t1)
        {
            if (t1 == true)
            {
                WhoWon = T1;
            }
            else WhoWon = T2;
        }

    }

}
