using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace People
{
    public class Player : Person///Ready
    {
        public Player(string imie, string nazwisko)
        {
            Name = imie;
            Surname = nazwisko;
        }
        public override string ToString()
        {
            return Name + " " + Surname;
        }
    }
}
