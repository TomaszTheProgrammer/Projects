using System.Collections.Generic;

namespace ClassLibrary
{
    public class Contact
    {
        public Person Person { get; private set; }
        public Location Location { get; private set; }
        public PhoneNumber Number { get; set; }

        public Contact(Person Person, Location Location, PhoneNumber Number)
        {
            this.Person = Person;
            this.Location = Location;
            this.Number = Number;
        }
    }
}
