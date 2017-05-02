using ClassLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryTests
{
    class DatabaseConnectionTests
    {
        [TestClass]
        public class DataManagementTests
        {
            private DataContext DC;
            private DataManagement DM;
            private DataService DS;

            private Person person;
            private Location location;
            private StationaryPhoneNumber stationaryNumber;
            private MobilePhoneNumber mobileNumber;
            private Contact contact;
            private string name = "Name";
            private string surname = "Surname";
            private string city = "City";
            private string zipCode = "ZipCode";
            private string mNumber = "601602603";
            private string sNumber = "426010011";
            private string server = "LAPTOP-TOMASZ\\SQLEXPRESS";
            private string database = "PhoneBook";

            [TestInitialize]
            public void Startup()
            {
                DC = new DataContext(server, database);
                DM = new DataManagement(DC);
                DS = new DataService(DM);
                location = new Location(city, zipCode);
                person = new Person(name, surname, location);
                stationaryNumber = new StationaryPhoneNumber(sNumber);
                mobileNumber = new MobilePhoneNumber(mNumber);
                contact = new Contact(person, mobileNumber);
            }

            [TestCleanup]
            public void Cleanup()
            {
                DC = null;
                DM = null;
                DS = null;
                person = null;
                location = null;
                stationaryNumber = null;
                mobileNumber = null;
                contact = null;
            }

            [TestMethod]
            public void InsertAndDelete()
            {
                DM.Insert(contact);
                int p = DM.CountRows("People");
                Assert.AreEqual(1, DM.DoesExistContact(contact));
                DM.Remove(contact);
                Assert.AreEqual(0, DM.DoesExistContact(contact));
            }
        }
    }
}
