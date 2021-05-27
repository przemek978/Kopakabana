using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kopakabana
{
    class ExistNameException:TourException
    {
        string Surname;
        public ExistNameException(string name):base(name) { }
        public ExistNameException(string mes,string name) : base(mes, name) { }
        public ExistNameException(string mes, string name,string surname) : base(mes, name)
        {
            Surname = surname;
        }
        public string getSurname()
        {
            return Surname;
        }

    }
}
