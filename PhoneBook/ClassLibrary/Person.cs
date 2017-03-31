using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Person
    {
        public Person(string Name, string Surname, Location Location)
        {
            this.Name = Name;
            this.Surname = Surname;
            this.Location = Location;
            this.UUID = Guid.NewGuid();
        }

        public Guid UUID { get; private set; }
        public String Name { get; private set; }
        public String Surname { get; private set; }
        public Location Location { get; private set; }
    }
}
