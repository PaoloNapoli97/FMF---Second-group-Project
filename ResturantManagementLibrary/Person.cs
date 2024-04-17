namespace ResturantManagementLibrary
{
    public class Person
    {
        public string Name { get; set; } = String.Empty;
        public string LastName { get; set; } = String.Empty;
        private string _email = String.Empty;
        private string _phone = String.Empty;
        private static int _id = 0; //? is for internal purpose (if email is empty field)

        public Person(string name, string lastName, string email, string phone)
        {
            Name = name;
            LastName = lastName;
            Email = email;
            Phone = phone;
        }

        public string Email
        {
            get { return _email; }
            set
            {
                if (value == "")
                {
                    _email = $"User{_id}@email.com";
                    _id++;
                }
                else
                {
                    _email = value;
                }
            }
        }

        public string Phone
        {
            get { return _phone; }
            set
            {
                if (value == "")
                {
                    _phone = "Empty phone";
                }
                else
                {
                    _phone = value;
                }
            }
        }
    }
}