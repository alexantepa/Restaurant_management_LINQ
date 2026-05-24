using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant_management
{
    public class Dish
    {
        public int id;
        public string name;
        public int price;
        public CuisineType cuisine;

        public Dish(int id, string name, int price, CuisineType cuisine)
        {
            this.id = id;
            this.name = name;
            this.price = price;
            this.cuisine = cuisine;
        }
    }
}
