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
            string server = ".\\SQLEXPRESS";
            string database = "PhoneBook";
            DataManagement DM = new DataManagement(new DataContext(server, database));
            DM.Connect();
            Location l = new Location("Lodz", "00-000");
            Person p = new Person("Tomasz", "Smolarek", l);
            PhoneNumber n = new MobilePhoneNumber("607890726");
            Contact c = new Contact(p, n);

            DM.Disconnect();
        }
    }
}
