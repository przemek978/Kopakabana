using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace People
{
    public abstract class Person
    {
        public string Name, Surname;

        public string getName()
        {
            return this.Name;
        }

        public string getSurname()
        {
            return this.Surname;
        }

        public void setName(string NewName)
        {
            this.Name = NewName;
        }
        public void setSurname(string NewSurname)
        {
            this.Surname = NewSurname;
        }




    }

}
