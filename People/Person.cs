using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace People
{
    public abstract class Person///Ready
    {
        protected string Name, Surname;

        public Person(string imie,string nazwisko)
        {
            Name = imie;
            Surname = nazwisko;
        }
        //Dostep do imienia
        public string GetName()
        {
            return this.Name;
        }
        public void SetName(string NewName)
        {
            this.Name = NewName;
        }
        //Dostep do nazwiska
        public string GetSurname()
        {
            return this.Surname;
        }
        public void SetSurname(string NewSurname)
        {
            this.Surname = NewSurname;
        }




    }

}
