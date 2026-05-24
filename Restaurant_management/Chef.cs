using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_management
{
    public class Chef
    {
        public string name;
        public int ordersHandled;
        public CuisineType typeOfCuisine;
        public int currentOrdersCount = 0;
        public const int MaxConcurrentOrders = 3;
        private readonly Queue<Order> pendingOrders = new Queue<Order>();

        public Chef(string name, int ordersHandled, CuisineType typeOfCuisine)
        {
            this.name = name;
            this.ordersHandled = ordersHandled;
            this.typeOfCuisine = typeOfCuisine;
        }

        public bool CanAcceptOrder()
        {
            return currentOrdersCount < MaxConcurrentOrders;
        }

        public async Task StartCookingAsync(Order order)
        {
            currentOrdersCount++;
            Console.WriteLine($"Шеф {name} начал готовить заказ #{order.id} (текущих заказов: {currentOrdersCount}/{MaxConcurrentOrders})");
            
            await order.SimulateCookingAsync();
            
            currentOrdersCount--;
            ordersHandled++;
            Console.WriteLine($"Шеф {name} завершил заказ #{order.id} (текущих заказов: {currentOrdersCount}/{MaxConcurrentOrders})");
            
            // Если есть ожидающие заказы, начинаем готовить следующий
            if (pendingOrders.Count > 0)
            {
                var nextOrder = pendingOrders.Dequeue();
                _ = StartCookingAsync(nextOrder);
            }
        }

        public void AddToPendingQueue(Order order)
        {
            pendingOrders.Enqueue(order);
            Console.WriteLine($"Заказ #{order.id} добавлен в очередь к шефу {name} (в очереди: {pendingOrders.Count})");
        }
    }
}
