using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class DataManagement
    {
        public DataContext data;
        
        public DataManagement(DataContext data)
        {
            if (data != null)
            {
                this.data = data;
            }
            else throw new NullReferenceException(data.GetType().ToString() + " is null");
        }

        public void Add(Person person)
        {
            data.People.Add(person.UUID, person);
        }

        public void Add(PhoneNumber number)
        {
            data.Numbers.Add(number);
        }

        public void Add(Contact contact)
        {
            data.Contacts.Add(contact);
        }

        public void Delete(Person person)
        {
            data.Contacts.Remove(data.Contacts.Find(contact => contact.Person == person));
            data.People.Remove(person.UUID);
        }

        public void Delete(PhoneNumber number)
        {
            data.Numbers.Remove(number);
        }

        public void Delete(Contact contact)
        {
            data.Contacts.Remove(contact);
        }
    }
}
