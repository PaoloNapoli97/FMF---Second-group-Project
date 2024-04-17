using System.Globalization;
using FileManager.Controller;
using static ResturantManagementLibrary.Employee;

namespace ResturantManagementLibrary
{
    class LoginMenu
    {
        private static LoginMenu instance;
        private LoginMenu() { }

        public static LoginMenu GetInstance()
        {
            if (instance == null)
            {
                instance = new();
            }
            return instance;
        }

        public void StartLoginMenu()
        {
            string[] options = { "1. Login", "2. Sign Up", "0. Exit" };
            int selectOption;

            //? Create and seed databases
            EmployeeController employeeController = new();
            employeeController.CreateEmployeeDb();

            TableFileManager tableFileManager = new();
            tableFileManager.CreateTablesDb();

            do
            {
                MenuUtils.ShowMenuOption(options);
                selectOption = MenuUtils.ReadChoise();
                switch (selectOption)
                {
                    case 1: //? Login
                        if (LoginForm() == true)
                        {
                            Console.Clear();
                            MainMenu mainMenu = new();
                            mainMenu.StartMainMenu(); //? start main menu
                        }
                        else
                        {
                            StartLoginMenu();
                        }
                        break;
                    case 2: //? Sign up 
                        SingUpForm();
                        break;
                    case 0:
                        Console.Clear();
                        Console.WriteLine($"Exit from program...");
                        break;
                    default:
                        Console.WriteLine($"Wrong option!");
                        break;
                }
            } while (selectOption != 1 && selectOption != 2 && selectOption != 0);
        }

        public bool LoginForm()
        {
            Console.WriteLine($"Employee Login");

            Console.WriteLine($"Enter your email: ");
            string? email = Console.ReadLine();

            Console.WriteLine($"Enter your password: ");
            string? password = Console.ReadLine();

            if (EmployeeController.IsLogged(email, password))
            {
                Console.WriteLine($"Login successful!");
                return true;
            }
            else
            {
                Console.Clear();
                Console.WriteLine($"Invalid email or password. Login failed!");
                return false;
            }
        }

        public void SingUpForm()
        {
            Console.Clear();
            Console.WriteLine($"Employee Sign up");

            Console.WriteLine($"Enter your name: ");
            string name = Console.ReadLine();

            Console.WriteLine($"Enter your last name: ");
            string lastName = Console.ReadLine();

            Console.WriteLine($"Enter your email: ");
            string email = Console.ReadLine();

            Console.WriteLine($"Enter your phone: ");
            string phone = Console.ReadLine();

            Console.WriteLine($"Enter your password: ");
            string password = Console.ReadLine();
            RoleList role = ChoiseRole();
            TimeSpan date = CalculateWorkHours();
            EmployeeController.NewEmployee(name, lastName, email, phone, password, role, date);
            Console.Clear();
            Console.WriteLine($"The {name} employee was created");
            StartLoginMenu();
        }

        public TimeSpan CalculateWorkHours()
        {
            TimeSpan duration = TimeSpan.Zero;
            Console.WriteLine("Enter the starting hours of your shift (HH:mm:ss): ");
            string inputStart = Console.ReadLine();
            TimeSpan timeSpanStart;
            if (TimeSpan.TryParse(inputStart, out timeSpanStart))
            {
                Console.WriteLine("Enter the starting hours of your shift (HH:mm:ss): ");
                string inputEnd = Console.ReadLine();
                TimeSpan timeSpanEnd;
                if (TimeSpan.TryParse(inputEnd, out timeSpanEnd))
                {
                    duration = timeSpanEnd - timeSpanStart;
                }
                else
                {
                    Console.WriteLine("Formato orario non valido. Riprova.");
                }
            }
            else
            {
                Console.WriteLine("Formato orario non valido. Riprova.");
            }
            return duration;
        }

        public static void PrintRole()
        {
            Console.WriteLine($"Role list:");
            foreach (var role in Enum.GetValues(typeof(RoleList)))
            {
                Console.WriteLine($"- {(int)role}. {role}");
            }
        }

        public static RoleList ChoiseRole() //? to learn
        {
            RoleList roleChoised = RoleList.WithoutRole;

            do
            {
                PrintRole();
                Console.WriteLine($"Choise a role for new employee (with num)!");
                if (int.TryParse(Console.ReadLine(), out int choise) && Enum.IsDefined(typeof(RoleList), choise))
                {
                    roleChoised = (RoleList)choise;
                    Console.WriteLine($"You are choise: {roleChoised}");
                }
                else
                {
                    Console.WriteLine($"Choise not valid!");
                }
            } while (!Enum.IsDefined(typeof(RoleList), roleChoised));

            return roleChoised;

        }
    }
}