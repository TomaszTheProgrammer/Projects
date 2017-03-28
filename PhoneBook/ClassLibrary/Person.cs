﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Person
    {
        public Person(string Name, string Surname)
        {
            this.Name = Name;
            this.Surname = Surname;
            this.UUID = Guid.NewGuid();
        }

        public Guid UUID { get; private set; }
        public String Name { get; private set; }
        public String Surname { get; private set; }
    }
}
