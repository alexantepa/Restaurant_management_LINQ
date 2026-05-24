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
        private bool isCooking = false;

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
            // Добавляем заказ в очередь и запускаем обработку
            pendingOrders.Enqueue(order);
            Console.WriteLine($"Заказ #{order.id} добавлен к шефу {name} (в очереди: {pendingOrders.Count})");
            await ProcessQueueAsync();
        }

        public async Task AddOrderAsync(Order order)
        {
            // Добавляем заказ в очередь
            pendingOrders.Enqueue(order);
            Console.WriteLine($"Заказ #{order.id} добавлен в очередь к шефу {name} (в очереди: {pendingOrders.Count})");
            
            // Пытаемся обработать очередь
            await ProcessQueueAsync();
        }

        private async Task ProcessQueueAsync()
        {
            // Запускаем заказы из очереди пока есть место и заказы в очереди
            while (pendingOrders.Count > 0 && currentOrdersCount < MaxConcurrentOrders)
            {
                var nextOrder = pendingOrders.Dequeue();
                // Запускаем задачу приготовления
                _ = CookOrderAsync(nextOrder);
            }
        }

        private async Task CookOrderAsync(Order order)
        {
            currentOrdersCount++;
            Console.WriteLine($"Шеф {name} начал готовить заказ #{order.id} (текущих заказов: {currentOrdersCount}/{MaxConcurrentOrders})");
            
            await order.SimulateCookingAsync();
            
            currentOrdersCount--;
            ordersHandled++;
            Console.WriteLine($"Шеф {name} завершил заказ #{order.id} (текущих заказов: {currentOrdersCount}/{MaxConcurrentOrders})");
            
            // После завершения заказа проверяем очередь
            await ProcessQueueAsync();
        }
    }
}
