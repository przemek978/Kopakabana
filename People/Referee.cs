using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace People
{
    public class Referee:Person
    {

        public Referee(string imie, string nazwisko):base(imie,nazwisko)
        {
            
        }
        public override string ToString()
        {
            return Name + " " + Surname;
        }
        public bool Equals(Referee sedzia)
        {
            if (sedzia.getName() == Name && sedzia.getSurname() == Surname)
                return true;
            else
                return false;
        }
    }
}
