using Restaurant_management;

Restaurant restaurant = new Restaurant();

restaurant.dishes.Add(new Dish(1, "Пицца", 600, CuisineType.Italian));
restaurant.dishes.Add(new Dish(2, "Суши", 800, CuisineType.Japanese));

restaurant.waiters.Add(new Waiter { id = 1, name = "Иван", ordersHandled = 0 });
restaurant.waiters.Add(new Waiter { id = 2, name = "Мария", ordersHandled = 0 });

restaurant.chefs.Add(new Chef("Алексей", 0, CuisineType.Italian));
restaurant.chefs.Add(new Chef("Светлана", 0, CuisineType.Japanese));

restaurant.customer.Add(new Customer(1, "Петр"));
restaurant.customer.Add(new Customer(2, "Анна"));

while (true)
{
    Console.WriteLine("1. Сделать заказ\n2. Показать самый епопулярный блюда\n" +
        "3. Показать отчет о работе официантов\n4. Показать информацию о клиентах\n5. Выйти");
    int choice = int.Parse(Console.ReadLine());
    if (choice == 1)
    {
        Console.Write("Введите номер столика: ");
        int table = int.Parse(Console.ReadLine());

        Console.WriteLine("Выберите id официанта: ");
        foreach (var w in restaurant.waiters)
        {
            Console.WriteLine($"{w.id}. {w.name} ");
        }
        int waiter = int.Parse(Console.ReadLine());

        Console.WriteLine("Выберите id клиента: ");
        foreach (var c in restaurant.customer)
        {
            Console.WriteLine($"{c.id}. {c.name} ");
        }
        int customer = int.Parse(Console.ReadLine());
        Order orderMy = new Order()
        {
            waiterID = waiter,
            customerID = customer,
            tableNumber = table
        };

        while (true)
        {
            Console.WriteLine("Выберите id блюда (0 для завершения заказа): ");
            foreach (var d in restaurant.dishes)
            {
                Console.WriteLine($"{d.id}. {d.name} - {d.price} руб.");
            }
            int dishId = int.Parse(Console.ReadLine());
            if (dishId == 0) {
                restaurant.TakeOrder(orderMy);
                break;
            }
            Dish dish = restaurant.FindOfDish(dishId);
            if (dish != null)
            {
                orderMy.AddDish(dish);

                Console.WriteLine("Блюдо добавлено в заказ.\n");
            }
            else
            {
                Console.WriteLine("Неверный id блюда. Попробуйте снова.");
            }
        }
    }
    else if (choice == 2)
    {
        restaurant.ShowPopularDishes();
    }
    else if (choice == 3)
    {
        restaurant.ShowHardworkingWaits();
    }
    else if (choice == 4)
    {
        restaurant.ShowReportAboutCustomers();
    }
    else if (choice == 5)
    {
        Environment.Exit(0);
    }
}
