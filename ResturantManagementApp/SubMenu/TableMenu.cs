using FileManager.Controller;

namespace ResturantManagementLibrary
{
    class TableMenu
    {
        TableFileManager tableFileManager = new();
        MenuUtils menuUtils = new();
        public void StartTableMenu()
        {
            List<Table> tables = tableFileManager.ReadTable();
            Console.Clear();

            string[] options = 
            {
                "1. Show Table status",
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
                    menuUtils.ShowTable(tables);
                    break;

                    default:
                    break;
                }
            } while (selectOption != 1 && selectOption != 0);
        }
    }
}