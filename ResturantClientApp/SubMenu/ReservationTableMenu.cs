using System.Security.Cryptography.X509Certificates;
using FileManager.Controller;
using ResturantManagementLibrary;

namespace ResturantClientApp
{
    class ReservationTableMenu
    {
        MainMenu mainMenuClient = MainMenu.GetInstance();
        ReservationTableFileManager reservationTableFileManager = new();
        TableFileManager tableFileManager = new();

        public void StartReservationMenu()
        {
            Console.Clear();

            string customerId = MenuUtils.CustomerForm();

            string[] options =
            {
                "1. Make a reservation",
                "0. Back to main menu"
            };
            int selectOption;

            do
            {
                MenuUtils.ShowMenuOption(options);
                selectOption = MenuUtils.ReadChoise();
                switch (selectOption)
                {
                    case 1:
                        CreateReservationTableForm(customerId);
                        break;
                    case 0:
                        Console.Clear();
                        Console.WriteLine($"Backing to main menu...");
                        mainMenuClient.StartMainMenu();
                        break;
                    default:
                        Console.WriteLine($"Wrong option!");
                        break;
                }
            } while (selectOption != 1 && selectOption != 0);

        }

        public void CreateReservationTableForm(string customerId)
        {
            List<Table> tables = tableFileManager.ReadTable();

            MenuUtils.ShowAvailableTables(tables);

            Console.WriteLine($"Enter a table id: ");
            string tableId = Console.ReadLine();

            tableFileManager.ChangeStatusTable(tableId);

            //TODO: add select time form
            DateTime startTime = DateTime.Now;
            DateTime endTime = startTime.AddHours(4);

            reservationTableFileManager.AddReservation(customerId, tableId, startTime, endTime);
        }
    }
}