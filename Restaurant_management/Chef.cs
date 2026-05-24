using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant_management
{
    public class Chef
    {
        public string name;
        public int ordersHandled;
        public CuisineType typeOfCuisine;

        public Chef(string name, int ordersHandled, CuisineType typeOfCuisine)
        {
            this.name = name;
            this.ordersHandled = ordersHandled;
            this.typeOfCuisine = typeOfCuisine;
        }
    }
}
