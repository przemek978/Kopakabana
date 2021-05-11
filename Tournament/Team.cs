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
        string Name;
        Player P1, P2, P3, P4;
        public Team(string name,Player p1, Player p2, Player p3, Player p4)
        {
            Name = name;
            P1 = p1;
            P2 = p2;
            P3 = p3;
            P4 = p4;
        }
        public string getName()
        {
            return Name;
        }
        public override string ToString()
        {
            return Name+"\n"+P1.ToString() + "\n"+ P2.ToString() + "\n"+ P3.ToString() + "\n"+ P4.ToString() ;
        }
    }
}
