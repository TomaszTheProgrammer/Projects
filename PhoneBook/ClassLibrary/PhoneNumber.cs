using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public abstract class PhoneNumber
    {
        public string Number { get; set; }
        public abstract void GenerateNewNumber();
        public abstract string RandomDigits(int length);
       
    }
}
