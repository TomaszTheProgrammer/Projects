using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program
{
    class Program
    {
        static void Main(string[] args)
        {
            Contact c = new Contact(new Person("Tomasz", "Smolarek"), new Location("Lodz", "91-035"), new MobilePhoneNumber("607890726"));
        }
    }
}
