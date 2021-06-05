using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kopakabana
{
    class TourException:Exception
    {
        protected string Name;
        public TourException() { }
        public TourException(string mes):base(mes) { }
        //public TourException(string name) { Name=name;}
        public TourException(string mes,string name) : base(mes) { Name = name; }
        public string GetName()
        {
            return Name;
        }
    }

}
