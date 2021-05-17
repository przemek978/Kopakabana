using Kopakabana;
using People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matches
{
    public abstract class Match
    {
       public Team T1, T2;
       Referee Ref;
        public override string ToString()
        {
            return T1.getName() + " - " + T2.getName();
        }
    }
}
