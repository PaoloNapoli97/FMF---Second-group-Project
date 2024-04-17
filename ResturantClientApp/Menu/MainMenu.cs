using ResturantManagementLibrary;

namespace ResturantClientApp
{
    class MainMenu
    {
        private static MainMenu instance;
        private MainMenu() { }

        public static MainMenu GetInstance()
        {
            if (instance == null)
            {
                instance = new MainMenu();
            }
            return instance;
        }

        public void StartMainMenu()
        {
            string[] options =
            {
                "1. Reseve a table",
                "2. Order a dish",
                "0. Exit"
            };
            int selectOption;

            do
            {
                ResturantManagementLibrary.MenuUtils.ShowMenuOption(options);
                selectOption = ResturantManagementLibrary.MenuUtils.ReadChoise();
                switch (selectOption)
                {
                    case 1:
                        ReservationTableMenu reservationTableMenu = new();
                        reservationTableMenu.StartReservationMenu();
                        break;
                    case 2:
                        OrderDishMenu orderDishMenu = new();
                        orderDishMenu.StartDishMenu();
                        break;
                    case 0:
                        Console.Clear();
                        Console.WriteLine($"Exit...");
                        return;
                    default:
                        Console.WriteLine($"Wrong option!");
                        break;
                }
            } while (selectOption != 1 && selectOption != 2 && selectOption != 0);

        }

    }
}