using ResturantManagementLibrary;

namespace FileManager.Controller
{
    public class TableFileManager
    {
        private const string tablesDbPath = "../FileManager/Database/TablesDb.csv";

        public void CreateTablesDb()
        {
            try
            {
                if (!File.Exists(tablesDbPath))
                {
                    using (StreamWriter file = File.CreateText(tablesDbPath))
                    {
                        file.WriteLine("- Tables Database");
                        file.WriteLine("TableId | Capacity | IsAvailable");
                        file.WriteLine("TableA1 | 2 | true");
                        file.WriteLine("TableA2 | 2 | true");
                        file.WriteLine("TableB1 | 4 | true");
                        file.WriteLine("TableB2 | 4 | true");
                        file.WriteLine("TableC1 | 8 | true");
                        file.WriteLine("TableC2 | 8 | true");
                        file.WriteLine("TableD1 | 10 | true");
                        file.WriteLine("TableD2 | 10 | true");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new IOException($"An error occurred while creating file: {ex.Message}");
            }

        }

        public void ChangeStatusTable(string tableId)
        {
            try
            {
                var lines = File.ReadAllLines(tablesDbPath);
                bool found = false;

                for (int i = 1; i < lines.Length; i++) // Start from 1 instead of 2
                {
                    var chunks = lines[i].Split('|');
                    if (chunks.Length >= 3 && chunks[0].Trim().ToLower() == tableId.Trim().ToLower())
                    {
                        bool currentStatus = bool.Parse(chunks[2].Trim());
                        chunks[2] = (!currentStatus).ToString().ToLower();
                        found = true;
                    }
                    lines[i] = string.Join(" | ", chunks);
                }

                if (!found)
                {
                    Console.WriteLine($"Table with id {tableId} was not found!");
                    return;
                }

                File.WriteAllLines(tablesDbPath, lines);
            }
            catch (Exception ex)
            {
                throw new IOException($"An error occurred while writing file: {ex.Message}");
            }
        }


        public List<Table> ReadTable()
        {
            List<Table> tables = new();
            using var input = File.OpenText(tablesDbPath);
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

                string tableId = chunks[0].Trim();
                int capacity = Convert.ToInt32(chunks[1].Trim());
                bool isAvailable = Convert.ToBoolean(chunks[2].Trim());

                Table table = new(tableId, capacity, isAvailable);
                tables.Add(table);
            }
            return tables;
        }
    }
}