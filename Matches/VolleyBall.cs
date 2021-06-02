using People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matches
{
    public class VolleyBall : Match
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

        public void SetAssistants(Referee ref2, Referee ref3)
        {
            AS1 = ref2;
            AS2 = ref3;
        }
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
        public override void SetWhoWon()
        {
            if (Result1 > Result2 && Result1 == 3)
            {
                WhoWon = T1;
                AddWins(T1);

            }
            else if (Result1 < Result2 && Result2 == 3)
            {
                WhoWon = T2;
                AddWins(T2);
            }
            else
                WhoWon = null;
        }
        public override string ToString()
        {
            return T1.getName() + " - " + T2.getName() + " " + getResult1() + " : " + getResult2();
        }
    }
}
