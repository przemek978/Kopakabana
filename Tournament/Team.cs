using People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace People
{
    public class Team
    {
        Player P1, P2, P3, P4;
        public Team(Player p1, Player p2, Player p3, Player p4)
        {
            P1 = p1;
            P2 = p2;
            P3 = p3;
            P4 = p4;
        }
        public override string ToString()
        {
            return P1.ToString() + "\n"+ P2.ToString() + "\n"+ P3.ToString() + "\n"+ P4.ToString() ;
        }
    }
}
