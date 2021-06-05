using People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matches
{
    class TugOfWar : Match//Ready
    {
        public TugOfWar(Team t1, Team t2, Referee Ref)
        {
            T1 = t1;
            T2 = t2;
            REF = Ref;
        }
        public override void SetWhoWon()
        {
            if (Result1 > Result2 && Result1 == 1)
            {
                WhoWon = T1;
                /*if (WhatFinal != true && WhatSemi != true)
                    AddWins(T1);*/
            }
            else if (Result1 < Result2 && Result2 == 1)
            {
                WhoWon = T2;
                /*if (WhatFinal != true && WhatSemi != true)
                    AddWins(T2);*/
            }
            else
                WhoWon = null;
        }
        public override bool Equals(Match T)
        {
            if (T.GetTeam1().GetName() == GetTeam1().GetName() && T.GetTeam2().GetName() == GetTeam2().GetName())
                return true;
            else
                return false;
        }
        public override string ToString()
        {
            return T1.GetName() + " - " + T2.GetName() + " " + GetResult1() + " : " + GetResult2();
        }
    }

}
