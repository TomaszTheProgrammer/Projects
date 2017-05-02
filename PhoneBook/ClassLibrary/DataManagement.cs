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

        ///<summary>Disconnects from database</summary>
        public void Disconnect()
        {
            data.connection.Close();
        }

        public void ConstraintOff(String table)
        {
            SqlCommand command = new SqlCommand("ALTER TABLE " + table + " NOCHECK CONSTRAINT ALL", data.connection);
            command.ExecuteNonQuery();
        }

        public void ConstraintOn(String table)
        {
            SqlCommand command = new SqlCommand("ALTER TABLE " + table + " CHECK CONSTRAINT ALL", data.connection);
            command.ExecuteNonQuery();
        }

        public void Insert(Contact c)
        {
            if(DoesExistNumber(c.Number)==1) c.Number.GenerateNewNumber();
            Connect();
            ConstraintOff(server + ".dbo.Contacts");
            InsertTuple(c.Person.Location);
            InsertTuple(c.Person);
            InsertTuple(c.Number);
            InsertTuple(c);
            ConstraintOn(server + ".dbo.Contacts");
            Disconnect();
        }

        public void Remove(Contact c)
        {
            Connect();
            ConstraintOff(server + ".dbo.Contacts");
            DeleteTuple(c);
            DeleteTuple(c.Number);
            DeleteTuple(c.Person);
            DeleteTuple(c.Person.Location);
            ConstraintOn(server + ".dbo.Contacts");
            Disconnect();
        }

        private void InsertTuple(Person p)
        {
            SqlCommand commandCheck = new SqlCommand("SELECT COUNT(id) FROM " + server + ".dbo.Locations WHERE id=" + p.Location.Id, data.connection);
            SqlCommand commandPerson = new SqlCommand("INSERT INTO " + server + ".dbo.People" + "(name, surname, id, id_location) " + "VALUES(@name, @surname, @id, @id_location)", data.connection);
            commandPerson.Parameters.Add("@name", System.Data.SqlDbType.VarChar, 20).Value = p.Name;
            commandPerson.Parameters.Add("@surname", System.Data.SqlDbType.VarChar, 20).Value = p.Surname;
            commandPerson.Parameters.Add("@id", System.Data.SqlDbType.VarChar, 36).Value = p.UUID.ToString();
            commandPerson.Parameters.Add("@id_location", System.Data.SqlDbType.Int).Value = p.Location.Id;
            int flag = (int)commandCheck.ExecuteScalar();
            if (flag == 0) InsertTuple(p.Location);
            commandPerson.ExecuteNonQuery();
        }

        private void InsertTuple(PhoneNumber p)
        {
            SqlCommand commandNumber = new SqlCommand("INSERT INTO " + server + ".dbo.Numbers" + "(number) " + "VALUES(@number)", data.connection);
            commandNumber.Parameters.Add("@number", System.Data.SqlDbType.VarChar, 20).Value = p.Number;
            commandNumber.ExecuteNonQuery();
        }

        private void InsertTuple(Location l)
        {
            SqlCommand commandLocation = new SqlCommand("INSERT INTO " + server + ".dbo.Locations" + "(id, city, zipcode) " + "VALUES(@id, @city, @zipcode)", data.connection);
            commandLocation.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = l.Id;
            commandLocation.Parameters.Add("@city", System.Data.SqlDbType.VarChar, 20).Value = l.City;
            commandLocation.Parameters.Add("@zipcode", System.Data.SqlDbType.VarChar, 20).Value = l.ZipCode;
            commandLocation.ExecuteNonQuery();
        }

        private void InsertTuple(Contact c)
        {
            SqlCommand commandContact = new SqlCommand("INSERT INTO " + server + ".dbo.Contacts" + "(id_person, id_number) " + "VALUES(@id_person, @id_number)", data.connection);
            commandContact.Parameters.Add("@id_person", System.Data.SqlDbType.VarChar, 36).Value = c.Person.UUID.ToString();
            commandContact.Parameters.Add("@id_number", System.Data.SqlDbType.VarChar, 20).Value = c.Number.Number;
            commandContact.ExecuteNonQuery();
        }

        private void DeleteTuple(Person p)
        {
            SqlCommand command = new SqlCommand("DELETE FROM " + server + ".dbo.People WHERE id=@id", data.connection);
            command.Parameters.Add("@id", System.Data.SqlDbType.VarChar, 36).Value = p.UUID.ToString();
            command.ExecuteNonQuery();
        }

        private void DeleteTuple(PhoneNumber p)
        {
            SqlCommand command = new SqlCommand("DELETE FROM " + server + ".dbo.Numbers WHERE number=@number", data.connection);
            command.Parameters.Add("@number", System.Data.SqlDbType.VarChar, 20).Value = p.Number;
            command.ExecuteNonQuery();
        }

        private void DeleteTuple(Location l)
        {
            SqlCommand command = new SqlCommand("DELETE FROM " + server + ".dbo.Locations WHERE id=@id", data.connection);
            command.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = l.Id;
            command.ExecuteNonQuery();
        }

        private void DeleteTuple(Contact c)
        {
            SqlCommand command = new SqlCommand("DELETE FROM " + server + ".dbo.Contacts WHERE id_person=@id_person", data.connection);
            command.Parameters.Add("@id_person", System.Data.SqlDbType.VarChar, 36).Value = c.Person.UUID.ToString();
            command.ExecuteNonQuery();
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

        public int DoesExistContact(Contact c)
        {
            SqlCommand commandCheck = new SqlCommand("SELECT COUNT(*) FROM " + server + ".dbo.Contacts WHERE id_person='" + c.Person.UUID.ToString()+"'", data.connection);
            Connect();
            int flag = (int)commandCheck.ExecuteScalar();
            Disconnect();
            return flag;
        }

        public int DoesExistNumber(PhoneNumber n)
        {
            SqlCommand commandCheck = new SqlCommand("SELECT COUNT(*) FROM " + server + ".dbo.Numbers WHERE number='" + n.Number + "'", data.connection);
            Connect();
            int flag = (int)commandCheck.ExecuteScalar();
            Disconnect();
            return flag;
        }

    }
}
