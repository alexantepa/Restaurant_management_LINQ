using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_management
{
    public class Restaurant
    {
        public List<Chef> chefs { get; set; } = new List<Chef>();
        public List<Order> orders { get; set; } = new List<Order>();
        public List<Dish> dishes { get; set; } = new List<Dish>();
        public List<Waiter> waiters { get; set; } = new List<Waiter>();
        public List<Customer> customer { get; set; } = new List<Customer>();

        public Dish FindOfDish(int id)
        {
            return dishes.FirstOrDefault(dish => dish.id == id);
        }

        public Chef FindChefForOrder(Order order)
        {
            foreach (var dish in order.dishes)
            {
                var chef = chefs
                    .Where(c => c.typeOfCuisine == dish.cuisine && c.CanAcceptOrder())
                    .OrderBy(c => c.currentOrdersCount)
                    .FirstOrDefault();

                if (chef != null)
                    return chef;
            }
            return null;
        }

        public async Task TakeOrderAsync(Order order)
        {
            orders.Add(order);

            // Сбрасываем статусы всех блюд в "Принято"
            foreach (var dish in order.dishes)
            {
                dish.Status = OrderStatus.Принято;
            }

            // Находим шефа для всего заказа
            Chef assignedChef = FindChefForOrder(order);
            
            if (assignedChef != null)
            {
                // Если шеф может принять заказ, начинаем готовить
                Console.WriteLine($"Заказ #{order.id} принят шефом {assignedChef.name}");
                await assignedChef.StartCookingAsync(order);
            }
            else
            {
                // Если нет свободного шефа, добавляем заказ в очередь ожидания к подходящему шефу
                var firstDish = order.dishes[0];
                var suitableChef = chefs.FirstOrDefault(c => c.typeOfCuisine == firstDish.cuisine);
                if (suitableChef != null)
                {
                    Console.WriteLine($"Заказ #{order.id} добавлен в очередь к шефу {suitableChef.name} (ожидает начала приготовления)");
                    await suitableChef.AddOrderAsync(order);
                }
                else
                {
                    Console.WriteLine($"Нет подходящего шефа для заказа #{order.id}");
                }
            }
        }

        public void ShowPopularDishes()
        {
            var popularity = orders.SelectMany(o => o.dishes)
                .GroupBy(d => d.name)
                .Select(g => new { Name = g.Key, Count = g.Count() })
                .OrderByDescending(x => x.Count);

            Console.WriteLine("Самые популярные блюда:");
            foreach (var item in popularity)
            {
                Console.WriteLine($"{item.Name}: {item.Count}");
            }
            Console.WriteLine("");
        }

        public void ShowHardworkingWaits()
        {
            var orderedWaiters = orders.GroupBy(o => o.waiterID)
                .Select(g => new
                {
                    name = waiters.First(w => w.id == g.Key).name,
                    count = g.Count()
                })
                .OrderByDescending(x => x.count);

            Console.WriteLine("Самые трудолюбивые официанты:");
            foreach (var w in orderedWaiters)
            {
                Console.WriteLine($"{w.name}: {w.count}");
            }
            Console.WriteLine("");
        }

        public void ShowReportAboutCustomers()
        {
            Console.WriteLine("Отчет о покупателях::");
            var report = orders.Join(
                customer,
                o => o.customerID,
                c => c.id,
                (order, customer) => new { o = order, c = customer })
                .GroupBy(x => x.c.name)
                .Select(g => new
                {
                    CustomerName = g.Key,
                    TotalDishes = g.Sum(x => x.o.totalPrice),
                    TotalOrdersCount = g.Count()
                });
            foreach (var item in report)
            {
                Console.WriteLine($"Покупатель: {item.CustomerName}," +
                    $" Общая сумма заказов: {item.TotalDishes}, " +
                    $"Кол-во заказов: {item.TotalOrdersCount}");
            }
            Console.WriteLine("");
        }
    }
}
