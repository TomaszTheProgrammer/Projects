using System.Collections.Generic;

namespace ClassLibrary
{
    public class Contact
    {
        public Person Person { get; private set; }
        public PhoneNumber Number { get; set; }

        public Contact(Person Person, PhoneNumber Number)
        {
            this.Person = Person;
            this.Number = Number;
        }
    }
}
