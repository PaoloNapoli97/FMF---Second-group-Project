namespace ResturantManagementLibrary
{
    public class MainMenu
    {
        LoginMenu loginMenu = LoginMenu.GetInstance();

        public void StartMainMenu()
        {
            string[] options =
            {
                "1. Dish Manager",
                "2. Orders Manager",
                "3. Tables Manager",
                "4. Employees Manager",
                "5. Warhouse Manage (not implemeted)",
                "6. Tax Manager (not implemeted)",
                "0. Exit"
            };
            int selectOption;

            do
            {
                MenuUtils.ShowMenuOption(options);
                selectOption = MenuUtils.ReadChoise();
                switch (selectOption)
                {
                    case 1: //? Dish Manager
                        Console.Clear();
                        DishMenu dishMenu = new();
                        dishMenu.StartDishMenu();
                        break;
                    case 2: //? Orders Manager
                        Console.Clear();
                        OrderMenu orderMenu = new();
                        orderMenu.StartOrderMenu();
                        StartMainMenu();
                        break;
                    case 3: //? Tables Manager
                    TableMenu tableMenu = new();
                    tableMenu.StartTableMenu();
                    break;
                    case 4: //? Employees Manager
                        Console.Clear();
                        EmployeeMenu employeeMenu = new();
                        employeeMenu.StartEmployeeMenu();
                        break;
                    case 5: //? Warhouse Manager
                        Console.WriteLine($"Not implement");
                        StartMainMenu();
                        break;
                    case 6: //? Tax Manager
                        Console.WriteLine($"Not implement");
                        StartMainMenu();
                        break;
                    case 0: //? Exit
                        loginMenu.StartLoginMenu();
                        break;
                    default:
                        Console.WriteLine($"Wrong option!");
                        break;
                }
            } while (
                        selectOption != 1 &&
                        selectOption != 2 &&
                        selectOption != 3 &&
                        selectOption != 4 &&
                        selectOption != 5 &&
                        selectOption != 6 &&
                        selectOption != 0
                    );
        }
    }
}