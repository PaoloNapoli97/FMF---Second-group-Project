using ResturantManagementLibrary;

namespace FileManager.Controller
{
    public class CheckFileManager
    {

        private const string CheckDbPath = "../FileManager/Database/CheckDb.csv";

        // CREATE CHECK DB IF DOESNT EXIST
        public void CreateCheckDb()
        {
            try
            {
                if (!File.Exists(CheckDbPath))
                {
                    using (StreamWriter file = File.CreateText(CheckDbPath))
                    {
                        file.WriteLine($"- Check Database");
                        file.WriteLine($"Dishes | CustomerId | Amount | Tip | Tax | IsPaid");
                    }
                }
            }
            catch (System.Exception ex)
            {
                throw new IOException("An error occurred while creating file: " + ex.Message);
            }
        }

        // READ CHECK DB

        // public List<Check> ReadCheck()
        // {
        //     List<Check> checks = new();
        //     using var input = File.OpenText(CheckDbPath);
        //     input.ReadLine();
        //     input.ReadLine();

        //     while (true)
        //     {
        //         string? line = input.ReadLine();

        //         if (line is null)
        //         {
        //             break;
        //         }

        //         var chunks = line.Split('|');

        //         var dishStrings = chunks[0].Split('-');

        //         Dictionary<Dish, int> orderedDishes = new();

        //         foreach (var dishString in dishStrings)
        //         {
        //             var dishChunks = dishString.Split('x');
        //             Dish dish = new Dish { Name = dishChunks[0].Trim(),  };
        //             int quantity = int.Parse(dishChunks[1]);
        //             orderedDishes[dish] = quantity;
        //         }
        //         string customerId = chunks[1].Trim();
        //         double amount = double.Parse(chunks[2].Trim());
        //         double tip = double.Parse(chunks[3].Trim());
        //         double tax = double.Parse(chunks[4].Trim());
        //         bool isPaid = bool.Parse(chunks[5].Trim());

        //         Check check = new(orderedDishes, customerId, amount, isPaid);
        //         checks.Add(check);
        //     }
        //     return checks;
        // }

        //? read dish list into check
        // private Dish ParseDish(string csvString)
        // {
        //     var dishChunks = csvString.Split(',');

        //     string name = dishChunks[0].Trim();
        //     string description = dishChunks[1].Trim();
        //     double price = double.Parse(dishChunks[2].Trim());
        //     Dish.CategoryList categoryList = Enum.TryParse(dishChunks[3].Trim(), out Dish.CategoryList parsedCategory) ? parsedCategory : Dish.CategoryList.NotCategory;
        //     var ingredientStrings = dishChunks[4].Split(';');
        //     List<IngredientManager.Ingredient> ingredients = new();

        //     foreach (var ingredientString in ingredientStrings)
        //     {
        //         if (Enum.TryParse(ingredientString.Trim(), out IngredientManager.Ingredient parsedIngredient))
        //         {
        //             ingredients.Add(parsedIngredient);
        //         }
        //     }
        //     return new Dish(name, description, price, categoryList, ingredients);
        // }

        //? make a order
        public void CreateDishOrder(Dictionary<Dish, int> selectedMenu, string customerId)
        {
            Check check = new(selectedMenu, customerId, 0.0, false);
            double amount = check.CalculateTotalAmout(selectedMenu);
            double taxed = check.CalcTax(amount);
            double tipsed = check.CalcTips(amount);
            amount = amount + taxed + tipsed;
            AddCheck(selectedMenu, customerId, amount, tipsed, taxed, true);
        }

        //? make a check
        public void AddCheck(Dictionary<Dish, int> orderedDishes, string customerId, double amount, double tip, double tax, bool isPaid)
        {
            using var output = File.AppendText(CheckDbPath);
            string dishlist = string.Join(", ", orderedDishes.Select(kv => $"{kv.Key.Name} x{kv.Value}"));
            output.WriteLine($"{dishlist} | {customerId} | {amount} | {tax} | {tip} | {isPaid}");
        }
    }
}