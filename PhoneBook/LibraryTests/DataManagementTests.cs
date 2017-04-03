using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassLibrary;

namespace LibraryTests
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
        public void AddPerson()
        {
            DM.Add(person);
            Assert.AreEqual(true, DC.People.ContainsKey(person.UUID));
        }

        [TestMethod]
        public void AddPhoneNumber()
        {
            DM.Add(stationaryNumber);
            DM.Add(mobileNumber);
            Assert.AreEqual(2, DC.Numbers.Count);
        }

        [TestMethod]
        public void AddContact()
        {
            DM.Add(contact);
            Assert.AreEqual(1, DC.Contacts.Count);
        }

        [TestMethod]
        public void DeletePerson()
        {
            DM.Delete(person);
            Assert.AreEqual(false, DC.People.ContainsKey(person.UUID));
            Assert.AreEqual(0, DC.Contacts.Count);
        }

        [TestMethod]
        public void DeletePhoneNumber()
        {
            DM.Delete(mobileNumber);
            Assert.AreEqual(0, DC.Numbers.Count);
        }

        [TestMethod]
        public void DeleteContact()
        {
            DM.Delete(contact);
            Assert.AreEqual(0, DC.Contacts.Count);
        }


    }
}
