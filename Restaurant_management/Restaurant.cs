    using System;
    using System.Collections.Generic;
    using System.Text;

    namespace Restaurant_management
    {
        public class Restaurant
        {
            public List<Chef> chefs { get; set; } = new List<Chef>();
            public List<Order> orders { get; set; } = new List<Order>();
            public List<Dish> dishes { get; set; } = new List<Dish>();
            public List<Waiter> waiters { get; set; } = new List<Waiter>();
            public List<Customer> customer { get; set; } = new List<Customer>();

            public Dish FindOfDish (int id)
            {
                return dishes.FirstOrDefault(dish => dish.id == id);
            }

            public void TakeOrder(Order order)
            {
                orders.Add(order);

                foreach (Dish dish in order.dishes)
                {
                    Chef chefBuff = chefs
                        .Where(c => c.typeOfCuisine == dish.cuisine && c.ordersHandled < 3)
                        .OrderBy(c => c.ordersHandled)
                        .FirstOrDefault();
                    if (chefBuff != null)
                    {
                        Console.WriteLine($"Блюдо {dish.name} готовит шеф {chefBuff.name}");
                        chefBuff.ordersHandled++;
                    }
                    else
                    {
                        Console.WriteLine($"Ожидание блюда более часа!");
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
