using System.Runtime.ConstrainedExecution;
using FileManager.Controller;
using ResturantManagementLibrary;

namespace ResturantClientApp
{
    class OrderDishMenu
    {
        MainMenu mainMenuClient = MainMenu.GetInstance();
        DishFileManager dishFileManager = new();
        CheckFileManager checkFileManager = new();

        public void StartDishMenu()
        {
            checkFileManager.CreateCheckDb();
            Console.Clear();

            string customerId = MenuUtils.CustomerForm();

            string[] options =
            {
                "1. Make a order",
                "0. Back to main menu"
            };

            int selectOption;

            do
            {
                MenuUtils.ShowMenuOption(options);
                selectOption = MenuUtils.ReadChoise();
                switch (selectOption)
                {
                    case 1: //? start menu
                        CreateDishOrderForm(customerId);
                        break;
                    case 0:
                        Console.Clear();
                        Console.WriteLine($"Backing to main menu...");
                        mainMenuClient.StartMainMenu();
                        break;
                    default:
                        Console.WriteLine($"Wrong option!");
                        break;
                }
            } while (selectOption != 1 && selectOption != 0);

        }

        public void CreateDishOrderForm(string customerId)
        {
            Console.Clear();

            CheckFileManager checkFileManager = new();
            List<Dish> dishes = dishFileManager.ReadDish();

            Dictionary<Dish, int> selectedMenu = new Dictionary<Dish, int>();

            string[] options = { "1. Print all dish", "2. Print by category", "0. Back." };
            int selectOption;

            do
            {
                MenuUtils.ShowMenuOption(options);
                selectOption = MenuUtils.ReadChoise();
                switch (selectOption)
                {
                    case 1:
                        Console.WriteLine($"Printing all dishes: ");
                        MenuUtils.ShowDishes(dishes);
                        break;
                    case 2:
                        Console.WriteLine($"Printing by category: ");
                        MenuUtils.ShowDishes(MenuUtils.ShowDishesByCategory(dishes));
                        break;
                    case 0:
                        Console.WriteLine($"Exit from order dish...");
                        StartDishMenu();
                        break;
                    default:
                        Console.WriteLine($"Wrong option!");
                        break;
                }

            } while (selectOption != 1 && selectOption != 2 && selectOption != 0);

            while (true)
            {
                Console.WriteLine($"Select a dish (with name) and enter 'end' to complete the order:");
                string dishName = Console.ReadLine();

                if (dishName.ToLower() == "end")
                {
                    break;
                }

                Dish selectedDish = dishes.FirstOrDefault(dish => dish.Name.Equals(dishName, StringComparison.OrdinalIgnoreCase));

                if (selectedDish != null)
                {
                    Console.WriteLine($"How many dish for {selectedDish.Name}: ");
                    int quantity = int.Parse(Console.ReadLine());
                    selectedMenu[selectedDish] = quantity;
                }
                else
                {
                    Console.WriteLine($"Dish not found. Try again.");
                }


            }
            checkFileManager.CreateDishOrder(selectedMenu, customerId);
            mainMenuClient.StartMainMenu();
        }
    }
}