using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kopakabana
{
    class NotSelectedException:TourException
    {
        public NotSelectedException(string mes):base(mes)
        {
        }
    }

}
