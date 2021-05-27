using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kopakabana
{
    class ExistNameException:TourException
    {
        public ExistNameException(string name):base(name) { }
        public ExistNameException(string mes,string name) : base(mes, name) { }
        
      
    }
}
