using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_management
{
    public class Order
    {
        private static int nextId = 1;
        public int id { get; set; }
        public int tableNumber { get; set; }
        public int customerID { get; set; }
        public int waiterID { get; set; }
        public List<Dish> dishes = new List<Dish>();
        public decimal totalPrice => dishes.Sum(d => d.price);
        public OrderStatus Status { get; set; } = OrderStatus.Принято;

        public Order()
        {
            id = nextId++;
        }

        public void AddDish(Dish dish) => dishes.Add(dish);

        public async Task SimulateCookingAsync()
        {
            Console.WriteLine($"\nЗаказ #{id}: Начало приготовления\n");
            
            var tasks = dishes.Select(dish => SimulateDishCookingAsync(dish));
            await Task.WhenAll(tasks);
            
            Console.WriteLine($"\nЗаказ #{id}: Все блюда готовы и вручены!\n");
        }

        private async Task SimulateDishCookingAsync(Dish dish)
        {
            Console.WriteLine($"[{dish.name}] Статус - {dish.Status}");
            
            await Task.Delay(1000);
            dish.Status = OrderStatus.Готовится;
            Console.WriteLine($"[{dish.name}] Статус - {dish.Status}");
            
            await Task.Delay(2000);
            dish.Status = OrderStatus.Готово;
            Console.WriteLine($"[{dish.name}] Статус - {dish.Status}");
            
            await Task.Delay(1000);
            dish.Status = OrderStatus.Вручено;
            Console.WriteLine($"[{dish.name}] Статус - {dish.Status}\n");
        }

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
