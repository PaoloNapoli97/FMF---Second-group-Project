namespace ResturantManagementLibrary
{
    public class Table
    {
        public string TableId { get; }
        public int Capacity { get; }
        public bool IsAvailable { get; }

        public Table(string tableId, int capacity, bool isAvailable)
        {
            TableId = tableId;
            Capacity = capacity;
            IsAvailable = isAvailable;
        }

    }
}