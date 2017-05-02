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
            Location l3 = new Location("Lodz", "20-000");
            Person p = new Person("Tomasz", "Smolarek", l);
            Person p2 = new Person("Learn", "C#", l2);
            Person p3 = new Person("Love", "It", l3);
            PhoneNumber n = new MobilePhoneNumber("607890726");
            PhoneNumber n2 = new MobilePhoneNumber("607890726");
            PhoneNumber n3 = new MobilePhoneNumber();
            Contact c = new Contact(p, n);
            Contact c2 = new Contact(p2, n2);
            Contact c3 = new Contact(p3, n3);

            DM.Insert(c);
            DM.Insert(c2);
            DM.Insert(c3);
            Console.WriteLine(DM.DoesExistContact(c));

            System.Threading.Thread.Sleep(5000);
            
            DM.Remove(c);
            DM.Remove(c2);
            DM.Remove(c3);

            Console.WriteLine(DM.DoesExistContact(c));

            Console.WriteLine(n3.Number);

            int count = DM.CountRows("Contacts");
            Console.WriteLine(count);
        }
    }
}
