namespace ResturantManagementLibrary
{
    public class Reservation
    {
        private string _customerId;
        private string _tableId;
        private DateTime _startDate;
        private DateTime _endDate;

        public Reservation(string customerId, string tableId, DateTime startDate, DateTime endDate)
        {
            _customerId = customerId;
            _tableId = tableId;
            _startDate = startDate;
            _endDate = endDate;
        }

        public DateTime StartDate
        {
            get
            {
                return _startDate;
            }
            set
            {
                if (value < DateTime.Now)
                {
                    throw new ArgumentException(string.Format("Start Date must not be in the past: {0}", value.ToString()));
                }
                else
                {
                    _startDate = value;
                }
            }
        }
        public DateTime EndDate
        {
            get
            {
                return _endDate;
            }
            set
            {
                if (value < DateTime.Now)
                {
                    throw new ArgumentException(string.Format("Start Date must not be in the past: {0}", value.ToString()));
                }
                else
                {
                    _endDate = value;
                }
            }
        }

        public string CustomerId { get => _customerId; set => _customerId = value; }
        public string TableId { get => _tableId; set => _tableId = value; }
    }
}