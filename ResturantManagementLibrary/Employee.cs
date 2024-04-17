namespace ResturantManagementLibrary
{
    public class Employee : Person
    {
        public enum RoleList
        {
            WithoutRole,
            Manager,
            Chef,
            Waiter
        }

        private string? _password;
        private RoleList _role;
        private TimeSpan _workingHours;

        public Employee(string name, string lastName, string email, string phone, string password, RoleList role, TimeSpan workingHours) : base(name, lastName, email, phone)
        {
            Password = password;
            Role = role;
            WorkingHours = workingHours;
        }

        public string? Password
        {
            get
            {
                if (string.IsNullOrEmpty(_password))
                {
                    throw new InvalidOperationException("Password is null!");
                }
                return _password;
            }
            set
            {
                _password = value;
                if (string.IsNullOrEmpty(_password))
                {
                    throw new ArgumentNullException(nameof(value), "Password can not be null!"); //Exception not working
                }
            }
        }

        public RoleList Role
        {
            get { return _role; }
            set
            {
                _role = value;
            }
        }

        public TimeSpan WorkingHours
        {
            get { return _workingHours; }
            set
            {
                _workingHours = value;
            }
        }
    }
}