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
        private string server;

        public DataManagement(DataContext data)
        {
            if (data != null)
            {
                this.data = data;
                this.server = data.connection.Database;
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
            SqlCommand command = new SqlCommand("ALTER TABLE " + table + " NOCHECK CONSTRAINT ALL", data.connection);
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
            SqlCommand commandCheck = new SqlCommand("SELECT COUNT(id) FROM " + server + ".dbo.Locations WHERE id=" + p.Location.Id, data.connection);

            SqlCommand commandPerson = new SqlCommand("INSERT INTO " + server + ".dbo.People" + "(name, surname, id, id_location) " + "VALUES(@name, @surname, @id, @id_location)", data.connection);
            commandPerson.Parameters.Add("@name", System.Data.SqlDbType.VarChar, 20).Value = p.Name;
            commandPerson.Parameters.Add("@surname", System.Data.SqlDbType.VarChar, 20).Value = p.Surname;
            commandPerson.Parameters.Add("@id", System.Data.SqlDbType.VarChar, 36).Value = p.UUID.ToString();
            commandPerson.Parameters.Add("@id_location", System.Data.SqlDbType.Int).Value = p.Location.Id;

            Connect();
            int flag = (int)commandCheck.ExecuteScalar();
            if (flag == 0) addLocation(p);
            commandPerson.ExecuteNonQuery();
            Disconnect();
        }

        private void addLocation(Person p)
        {
            SqlCommand commandLocation = new SqlCommand("INSERT INTO " + server + ".dbo.Locations" + "(id, city, zipcode) " + "VALUES(@id, @city, @zipcode)", data.connection);
            commandLocation.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = p.Location.Id;
            commandLocation.Parameters.Add("@city", System.Data.SqlDbType.VarChar, 20).Value = p.Location.City;
            commandLocation.Parameters.Add("@zipcode", System.Data.SqlDbType.VarChar, 20).Value = p.Location.ZipCode;
            commandLocation.ExecuteNonQuery();
        }

        public void DeleteTuple(Person p)
        {
            ConstraintOff(server + ".dbo.People");
            SqlCommand command = new SqlCommand("DELETE FROM " + server + ".dbo.People WHERE id=@id", data.connection);
            command.Parameters.Add("@id", System.Data.SqlDbType.VarChar, 36).Value = p.UUID.ToString();
            Connect();
            command.ExecuteNonQuery();
            Disconnect();
            ConstraintOn(server + ".dbo.People");
        }

        public void DeleteTuple(Location l)
        {
            SqlCommand command = new SqlCommand("DELETE FROM " + server + ".dbo.Locations WHERE id='" + l.Id + "'", data.connection);
            Connect();
            command.ExecuteNonQuery();
            Disconnect();
        }

        public int CountRows(String tableName)
        {
            int count = 0;
            SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM " + server + ".dbo." + tableName, data.connection);
            Connect();
            count = (int)command.ExecuteScalar();
            Disconnect();
            return count;
        }

        public int DoesExistPerson(Person p)
        {
            SqlCommand commandCheck = new SqlCommand("SELECT COUNT(id) FROM " + server + ".dbo.People WHERE id='" + p.UUID.ToString()+"'", data.connection);
            Connect();
            int flag = (int)commandCheck.ExecuteScalar();
            Disconnect();
            return flag;
        }

    }
}
