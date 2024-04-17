using ResturantManagementLibrary;

namespace FileManager.Controller
{
    public class ReservationTableFileManager
    {
        private const string reservationDbPath = "../FileManager/Database/ReservationDb.csv";

        public void CreateReservationDb()
        {
            try
            {
                if (!File.Exists(reservationDbPath))
                {
                    using (StreamWriter file = File.CreateText(reservationDbPath))
                    {
                        file.WriteLine("- Reservation Database");
                        file.WriteLine("CustomerId | TableId | StartTime | EndTIme");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new IOException($"An error occurred while creating file: {ex.Message}");
            }
        }

        public List<Reservation> ReadReservation()
        {
            List<Reservation> reservations = new List<Reservation>();
            using var input = File.OpenText(reservationDbPath);

            // Skip the header lines
            input.ReadLine();
            input.ReadLine();

            while (true)
            {
                string? line = input.ReadLine();

                if (line is null)
                {
                    break;
                }

                var chunks = line.Split('|');

                string customerId = chunks[0].Trim();
                string tableId = chunks[1].Trim();
                DateTime startTime = DateTime.Parse(chunks[2].Trim());
                DateTime endTime = DateTime.Parse(chunks[3].Trim());

                Reservation reservation = new(customerId, tableId, startTime, endTime);
                reservations.Add(reservation);
            }

            return reservations;
        }


        public void AddReservation(string customerId, string tableId, DateTime startDate, DateTime endDate)
        {
            try
            {
                using (StreamWriter file = File.AppendText(reservationDbPath))
                {
                    file.WriteLine($"{customerId} | {tableId} | {startDate} | {endDate}");
                }
            }
            catch (Exception ex)
            {
                throw new IOException($"An error occurred while adding a reservation: {ex.Message}");
            }
        }

    }
}