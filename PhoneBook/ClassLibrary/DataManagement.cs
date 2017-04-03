using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

        ///<summary>Connects to database</summary>
        public void Connect()   
        {
            data.connection.Open();
        }

        ///<summary>DIsconnects from database</summary>
        public void Disconnect()
        {
            data.connection.Close();
            
        }

        public void ConstraintOff(String table)
        {
            SqlCommand command = new SqlCommand("ALTER TABLE " + table +" NOCHECK CONSTRAINT ALL", data.connection);
            Connect();
            command.ExecuteNonQuery();
            Disconnect();
        }

        public void ConstraintOn(String table)
        {
            SqlCommand command = new SqlCommand("ALTER TABLE " + table + " CHECK CONSTRAINT ALL", data.connection);
            Connect();
            command.ExecuteNonQuery();
            Disconnect();
        }

        public void InsertTuple(Person p)
        {
            SqlCommand commandLocation = new SqlCommand("INSERT INTO PhoneBook.dbo.Locations" + "(id, city, zipcode) " + "VALUES(@id, @city, @zipcode)", data.connection);
            commandLocation.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = p.Location.Id;
            commandLocation.Parameters.Add("@city", System.Data.SqlDbType.VarChar, 20).Value = p.Location.City;
            commandLocation.Parameters.Add("@zipcode", System.Data.SqlDbType.VarChar, 20).Value = p.Location.ZipCode;

            SqlCommand commandPerson = new SqlCommand("INSERT INTO PhoneBook.dbo.People" + "(name, surname, id, id_location) " + "VALUES(@name, @surname, @id, @id_location)", data.connection);
            commandPerson.Parameters.Add("@name", System.Data.SqlDbType.VarChar, 20).Value = p.Name;
            commandPerson.Parameters.Add("@surname", System.Data.SqlDbType.VarChar, 20).Value = p.Surname;
            commandPerson.Parameters.Add("@id", System.Data.SqlDbType.VarChar, 36).Value = p.UUID.ToString();
            commandPerson.Parameters.Add("@id_location", System.Data.SqlDbType.Int).Value = p.Location.Id;

            Connect();
            commandLocation.ExecuteNonQuery();
            commandPerson.ExecuteNonQuery();
            Disconnect();
        }

        public void DeleteTuple(Person p)
        {
            ConstraintOff("PhoneBook.dbo.People");
            SqlCommand command = new SqlCommand("DELETE FROM PhoneBook.dbo.People WHERE name='" + p.Name+"' AND surname='" + p.Surname + "' AND id_location='" + p.Location.Id+"'", data.connection);
            DeleteTuple(p.Location);
            Connect();
            command.ExecuteNonQuery();
            Disconnect();
            ConstraintOn("PhoneBook.dbo.People");

        }

        public void DeleteTuple(Location l)
        {
            SqlCommand command = new SqlCommand("DELETE FROM PhoneBook.dbo.Locations WHERE id='" + l.Id + "'", data.connection);
            Connect();
            command.ExecuteNonQuery();
            Disconnect();
        }

        public int CountRows(String tableName)
        {
            int count = 0;
            SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM PhoneBook.dbo." + tableName, data.connection);
            Connect();
            count = (int)command.ExecuteScalar();
            Disconnect();
            return count;
        }

    }
}
