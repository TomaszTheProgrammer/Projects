using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
        public SqlConnection connection { get; private set; }
        

        public DataContext(string server, string database)
        {
            People = new Dictionary<Guid, Person>();
            Numbers = new List<PhoneNumber>();
            Contacts = new List<Contact>();
            connection = new SqlConnection();
            connection.ConnectionString = "Server=" + server + ";Database=" + database + ";Trusted_Connection=true";
        }
    }
}
