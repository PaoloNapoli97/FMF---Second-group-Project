using ResturantManagementLibrary;
using static ResturantManagementLibrary.Employee;

namespace FileManager.Controller
{
    public class EmployeeController
    {
        //DOING: read db
        //DONE: write db

        private const string employeeDbPath = "../FileManager/Database/EmployeeDb.csv";

        public void CreateEmployeeDb()
        {  //If File doesn't exist, create a new one
            try
            {
                if (!File.Exists(employeeDbPath))
                {
                    using (StreamWriter file = File.CreateText(employeeDbPath))
                    {
                        file.WriteLine($"- Employee Database");
                        file.WriteLine($"Name, LastName, Email, Phone, Password, Role, Working Time");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new IOException("An error occurred while creating file: " + ex.Message);
            }
        }
        private static List<Employee> ReadEmployees()
        {
            using var input = File.OpenText(employeeDbPath);
            input.ReadLine();
            input.ReadLine();

            List<Employee> employees = new();

            while (true)
            {
                string? line = input.ReadLine();

                if (line is null)
                {
                    break;
                }

                var chunks = line.Split(',');

                string name = chunks[0].Trim();
                string lastName = chunks[1].Trim();
                string email = chunks[2].Trim();
                string phone = chunks[3].Trim();
                string password = chunks[4].Trim();
                RoleList role = Enum.TryParse(chunks[5].Trim(), out RoleList parsedRole) ? parsedRole : RoleList.WithoutRole;
                TimeSpan workingHours = Convert.ToDateTime(chunks[6].Trim()).TimeOfDay;

                Employee employee = new(name, lastName, email, phone, password, role, workingHours);
                employees.Add(employee);
            }
            return employees;
        }

        //? Sign up
        public static void NewEmployee(string name, string lastName, string email, string phone, string password, Employee.RoleList role, TimeSpan workingHours)
        {
            using var output = File.AppendText(employeeDbPath);
            output.WriteLine($"{name}, {lastName}, {email}, {phone}, {password}, {role}, {workingHours}");
        }

        //? Login 
        public static bool IsLogged(string email, string password)
        {
            List<Employee> employees = ReadEmployees();
            Employee employeeLogged = employees.Find(e => e.Email == email && e.Password == password);

            return employeeLogged != null ? true : false;
        }

        public List<Employee> ReadPublicEmployee(){
            List<Employee> employees = ReadEmployees();
            return employees;
        }
    }
}