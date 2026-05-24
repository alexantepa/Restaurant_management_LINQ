using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant_management
{
    public class Order
    {
        public int id { get; set; }
        public int tableNumber { get; set; }
        public int customerID { get; set; }
        public int waiterID { get; set; }
        public List<Dish> dishes = new List<Dish>();
        public decimal totalPrice => dishes.Sum(d => d.price);

        public void AddDish(Dish id) => dishes.Add(id);
    }

}
