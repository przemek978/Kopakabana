using People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matches
{
    class VolleyBall : Match
    {
        Referee AS1, AS2;
        public int Result1, Result2;
        public VolleyBall(Team t1, Team t2)
        {
            T1 = t1;
            T2 = t2;          
        }

        public void SetAssistants(Referee ref2, Referee ref3)
        {
            AS1 = ref2;
            AS2 = ref3;
        }

        public int getResult1()
        {
            return Result1;
        }

        public void setResult1(int score1)
        {
            Result1 = score1;
        }

        public int getResult2()
        {
            return Result2;
        }

        public void setResult2(int score2)
        {
            Result2 = score2;
        }

        public override void SetWhoWon(bool t1)
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
    }
}
