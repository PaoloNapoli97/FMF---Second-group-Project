namespace ResturantManagementLibrary
{
    public class Customer : Person
    {
        private string? _address;
        private string _customerId;

        public Customer(string name, string lastName, string email, string phone, string address) : base(name, lastName, email, phone)
        {
            Address = address;
            CustomerId = $"{name}.{lastName}.{email}.{phone}.{address}";
        }

        public string? Address
        {
            get
            {
                if (string.IsNullOrEmpty(_address))
                {
                    throw new InvalidOperationException("Andress is null");
                }
                return _address;
            }
            set
            {
                _address = value;
                if (string.IsNullOrEmpty(_address))
                {
                    throw new ArgumentNullException(nameof(value), "Andress can not be null!");
                }
            }
        }

        public string CustomerId { get => _customerId; set => _customerId = value; }
    }
}