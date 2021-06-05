using People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matches
{
    public class VolleyBall : Match//Ready
    {
        protected Referee AS1, AS2;
        public VolleyBall(Team t1, Team t2, Referee R, Referee as1, Referee as2)
        {
            T1 = t1;
            T2 = t2;
            REF = R;
            AS1 = as1;
            AS2 = as2;
        }

        /*public void SetAssistants(Referee ref2, Referee ref3)
        {
            AS1 = ref2;
            AS2 = ref3;
        }*/
        public override void SetWhoWon()
        {
            if (Result1 > Result2 && Result1 == 3)
            {
                WhoWon = T1;
                /*if (WhatFinal != true && WhatSemi != true)
                    AddWins(T1);*/

            }
            else if (Result1 < Result2 && Result2 == 3)
            {
                WhoWon = T2;
               /* if (WhatFinal != true && WhatSemi != true)
                    AddWins(T2);*/
            }
            else
                WhoWon = null;
        }
        public override bool Equals(Match V)
        {
            if (V.GetTeam1().GetName() == GetTeam1().GetName() && V.GetTeam2().GetName() == GetTeam2().GetName())
                return true;
            else
                return false;
        }
        //Dostep do asystentow
        public void SetAssistant1(Referee ref2)
        {
            AS1 = ref2;
        }
        public void SetAssistant2(Referee ref3)
        {
            AS2 = ref3;
        }
        public Referee GetAssistant1()
        {
            return AS1;
        }
        public Referee GetAssistant2()
        {
            return AS2;
        }
        
        public override string ToString()
        {
            return T1.GetName() + " - " + T2.GetName() + " " + GetResult1() + " : " + GetResult2();
        }
    }
}
