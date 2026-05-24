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

        public void AddDish(Dish dish) => dishes.Add(dish);

        public void PrintCheck()
        {
            Console.WriteLine("========================");
            Console.WriteLine("      RESTAURANT");
            Console.WriteLine("========================");
            Console.WriteLine();
            Console.WriteLine($"Заказ #{id}");
            Console.WriteLine($"Дата: {DateTime.Now:dd.MM.yyyy HH:mm}");
            Console.WriteLine();

            foreach (var dish in dishes)
            {
                Console.WriteLine($"{dish.name,-15} {dish.price,4} Ft");
            }

            Console.WriteLine();
            Console.WriteLine("------------------------");
            Console.WriteLine($"Итог:             {totalPrice,4} Ft");
            Console.WriteLine();
            Console.WriteLine("Спасибо!");
            Console.WriteLine("========================");
        }
    }

}
