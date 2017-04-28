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
            Location l = new Location("Lodz", "00-000");
            Location l2 = new Location("Lodz", "10-000");
            Person p = new Person("Tomasz", "Smolarek", l);
            Person p2 = new Person("Learn", "C#", l2);
            PhoneNumber n = new MobilePhoneNumber("607890726");
            Contact c = new Contact(p, n);

            DM.InsertTuple(p);
            Console.WriteLine(DM.DoesExistPerson(p));

            System.Threading.Thread.Sleep(5000);

            DM.DeleteTuple(p);
            Console.WriteLine(DM.DoesExistPerson(p));

            int count = DM.CountRows("People");
            Console.WriteLine(count);
        }
    }
}
