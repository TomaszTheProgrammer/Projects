using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class StationaryPhoneNumber : PhoneNumber
    {
        public StationaryPhoneNumber(string Number)
        {
            this.Number = Number;
        }

        public StationaryPhoneNumber()
        {
            this.Number = RandomDigits(9);
        }

        public override void GenerateNewNumber()
        {
            this.Number = RandomDigits(9);
        }

        public override string RandomDigits(int length)
        {
            Random rnd = new Random();
            int digit = 0;
            string s = string.Empty;
            for (int i = 0; i < length; i++)
            {
                if (i == 0) while (digit == 0) digit = rnd.Next(10);
                s = String.Concat(s, digit.ToString());
            }
            return s;
        }
    }
}
