using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class DataContext
    {
        public Dictionary<Guid, Person> People;
        public List<PhoneNumber> Numbers;
        public List<Contact> Contacts;

        public DataContext()
        {
            People = new Dictionary<Guid, Person>();
            Numbers = new List<PhoneNumber>();
            Contacts = new List<Contact>();

        }
    }
}
