using  FileManager.Controller;
using static ResturantManagementLibrary.Employee;

namespace ResturantManagementLibrary
{
    public class EmployeeMenu
    {
        EmployeeController employeeController = new();
        MainMenu mainMenu = new();
        public void StartEmployeeMenu(){
            string[] options =
            {
                "1 - Show all employees",
                "0 - Exit"
            };

            int selectOption;

            do
            {
                MenuUtils.ShowMenuOption(options);
                selectOption = MenuUtils.ReadChoise();
                switch (selectOption)
                {
                    case 1:
                    PrintEmployees();
                    break;

                    case 0: //? back to main menu
                        mainMenu.StartMainMenu();
                        break;

                    default:
                        Console.WriteLine($"Wrong option!");
                        break;
                }
            } while (selectOption != 1);
        }

        public void PrintEmployees(){
            List<Employee> employees = employeeController.ReadPublicEmployee();

            Console.WriteLine("Employee List: ");
            foreach (var employee in employees)
            {
                Console.WriteLine($"Name: {employee.Name}, LastName: {employee.LastName}, Email: {employee.Email}, Password: **********, Working Hours: {employee.WorkingHours}");
            }
        }
    }
}