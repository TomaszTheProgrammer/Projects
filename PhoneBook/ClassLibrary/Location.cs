using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Location
    {
        public Location(string City, string ZipCode)
        {
            this.Id = id;
            this.City = City;
            this.ZipCode = ZipCode;
            id++;
        }
        private static int id = 0;
        public int Id { get; private set; }
        public string City { get; private set; }
        public string ZipCode { get; private set; }
    }
}
