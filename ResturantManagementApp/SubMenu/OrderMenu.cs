using FileManager.Controller;

namespace ResturantManagementLibrary
{
    class OrderMenu
    {
        MainMenu mainMenu = new();
        MenuUtils menuUtils = new();
        ReservationTableFileManager reservationTableFileManager = new();
        CheckFileManager checkFileManager = new();
        DishFileManager dishFileManager = new();

        public void StartOrderMenu()
        {
            dishFileManager.CreateDishDb();
            // List<Check> checks = checkFileManager.ReadCheck();
            Console.Clear();

            string[] options =
            {
                "1. Make a order",
                // "2. Print all orders",
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
                        CreateOrderTableForm();
                        break;
                    // case 2:
                    // menuUtils.ShowCheks(checks);
                    // break;
                    case 0:
                        Console.Clear();
                        Console.WriteLine($"Backing to main menu...");
                        mainMenu.StartMainMenu();
                        break;
                    default:
                        Console.WriteLine($"Wrong option!");
                        break;
                }
            } while (selectOption != 1 && selectOption != 0);


        }

        public void CreateOrderTableForm()
        {
            Console.Clear();

            List<Reservation> reservations = reservationTableFileManager.ReadReservation();
            List<Dish> dishes = dishFileManager.ReadDish();
            Dictionary<Dish, int> selectedMenu = new Dictionary<Dish, int>();

            string[] options = { "1. Print reservation", "0. Back..." };
            int selectOption;

            do
            {
                MenuUtils.ShowMenuOption(options);
                selectOption = MenuUtils.ReadChoise();
                switch (selectOption)
                {
                    case 1:
                        Console.WriteLine($"Printing all dishes: ");
                        MenuUtils.PrintAllReservation(reservations);
                        break;
                    case 0:
                        Console.WriteLine($"Exit from order dish...");
                        break;
                    default:
                        Console.WriteLine($"Wrong option!");
                        break;
                }

            } while (selectOption != 1 && selectOption != 0);

            Console.WriteLine("Select a reservation (by table ID):");
            string tableId = Console.ReadLine();
            string customerId = "not found"; //TODO: manage

            Reservation selectedReservation = reservations.FirstOrDefault(reservation => reservation.TableId.Equals(tableId, StringComparison.OrdinalIgnoreCase));

            if (selectedReservation != null)
            {
                customerId = selectedReservation.CustomerId + tableId;
            }
            else
            {
                Console.WriteLine("Table not found. Try again.");
            }

            MenuUtils.ShowDishes(dishes);

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
            mainMenu.StartMainMenu();
        }

    }
}