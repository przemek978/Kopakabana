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
    }
}
