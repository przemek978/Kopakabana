using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kopakabana
{
    class FileException:TourException
    {
        public FileException(string name) : base(name) { }
        public FileException(string mes,string name) : base(mes,name) { }
        
    }
}
