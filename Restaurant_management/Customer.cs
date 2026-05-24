using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant_management
{
    public class Customer
    {
        public int id;
        public string name;

        public Customer(int id, string name)
        {
            this.id = id;
            this.name = name;
        }
    }
}
