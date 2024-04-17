namespace ResturantManagementLibrary
{
    public class Check
    {
        public Dictionary<Dish, int> OrderedDishes { get; set; }
        private string _customerId;
        private double _amount;
        private bool _isPaid;

        public Check(Dictionary<Dish, int> orderedDishes, string customerId, double amount, bool isPaid)
        {
            OrderedDishes = orderedDishes;
            CustomerId = customerId;
            Amount = amount;
            IsPaid = isPaid;
        }

        public string? CustomerId
        {
            get
            {
                if (string.IsNullOrEmpty(_customerId))
                {
                    throw new InvalidOperationException("Andress is null");
                }
                return _customerId;
            }
            set
            {
                _customerId = value;
                if (string.IsNullOrEmpty(_customerId))
                {
                    throw new ArgumentNullException(nameof(value), "Andress can not be null!");
                }
            }
        }

        public double Amount
        {
            get
            {
                return _amount;
            }
            set
            {
                if (value < 0)
                {
                    throw new Exception("Value cannot be a negative");
                }
                else
                {
                    _amount = value;
                }
            }
        }

        public double CalcTax(double amount)
        {
            return amount * 0.22;
        }

        public double CalcTips(double amount)
        {
            return amount * 0.05;
        }

        public bool IsPaid
        {
            get
            {
                return _isPaid;
            }
            set { _isPaid = value; }
        }

        public double CalculateTotalAmout(Dictionary<Dish, int> selectedMenu)
        {
            double totalAmount = 0.0;

            foreach (var kvp in selectedMenu)
            {
                Dish dish = kvp.Key;
                int quantity = kvp.Value;
                double dishPrice = dish.Price;
                totalAmount += dishPrice * quantity;
            }
            return totalAmount;
        }

    }
}