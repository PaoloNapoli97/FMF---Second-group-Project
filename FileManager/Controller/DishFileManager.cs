using ResturantManagementLibrary;
using static ResturantManagementLibrary.IngredientManager;

namespace FileManager.Controller
{
    public class DishFileManager
    {
        //private const string dishDbPath = "/FileManager/Database/DishDb.csv";

        private const string dishDbPath = "/home/ludwig/workingRepos/ResturantManagementProject/FileManager/Database/DishDb.csv";
        public void CreateDishDb()
        {
            //? If File doesn't exist, create a new one
            try
            {
                if (!File.Exists(dishDbPath))
                {
                    using (StreamWriter file = File.CreateText(dishDbPath))
                    {
                        file.WriteLine($"- Dish Database");
                        file.WriteLine($"Name | Description | Price | Category | Ingredients");
                    }

                }
            }
            catch (Exception ex)
            {
                throw new IOException("An error occurred while creating file: " + ex.Message);

            }
        }

        public List<Dish> ReadDish()
        {
            List<Dish> dishes = new();
            using var input = File.OpenText(dishDbPath);
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

                string name = chunks[0].Trim();
                string description = chunks[1].Trim();
                double price = double.Parse(chunks[2].Trim());
                Dish.CategoryList categoryList = Enum.TryParse(chunks[3].Trim(), out Dish.CategoryList parsedCategory) ? parsedCategory : Dish.CategoryList.NotCategory;
                var ingredientStrings = chunks[4].Split(';');
                List<IngredientManager.Ingredient> ingredients = new();

                foreach (var ingredientString in ingredientStrings)
                {
                    if (Enum.TryParse(ingredientString.Trim(), out IngredientManager.Ingredient parsedIngredient))
                    {
                        ingredients.Add(parsedIngredient);
                    }
                }


                Dish dish = new(name, description, price, categoryList, ingredients);
                dishes.Add(dish);
            }
            return dishes;
        }

        //? CREATE
        public void AddDish(string name, string description, double price, Dish.CategoryList category, List<Ingredient> ingredients)
        {
            using var output = File.AppendText(dishDbPath);
            string ingredientList = string.Join("; ", ingredients.Select(ingredient => ((int)ingredient).ToString()));
            output.WriteLine($"{name} | {description} | {price} | {category} | {ingredientList}");
        }

        //? EDIT DB
        public void EditDishDB(string name, int selectOption, dynamic newValue)
        {
            try
            {
                var lines = File.ReadAllLines(dishDbPath);
                bool found = false;

                for (int i = 1; i < lines.Length; i++)
                {
                    var chunks = lines[i].Split('|');

                    if (chunks.Length >= 0 && chunks[0].Trim().ToLower() == name.Trim().ToLower())
                    {
                        switch (selectOption)
                        {
                            case 1:
                                chunks[0] = newValue;
                                break;

                            case 2:
                                chunks[1] = newValue;
                                break;

                            case 3:
                                chunks[2] = newValue.ToString();
                                break;

                            case 4:
                                chunks[3] = newValue.ToString();
                                break;

                            case 5:
                                chunks[4] = newValue.ToString();
                                break;

                            default:
                                Console.WriteLine($"Wrong option!");
                                break;
                        }


                        lines[i] = string.Join('|', chunks);
                        found = true;
                    }


                }

                if (!found) //? NOT FOUND
                {
                    Console.WriteLine($"Dish with name: {name} not found.");
                    return;
                }

                File.WriteAllLines(dishDbPath, lines);
            }
            catch (Exception ex)
            {
                throw new IOException("An error occurred while editing the file: " + ex.Message);
            }
        }

        //? CHECK IF FILE LIST CONTAIN NAME
        public bool DishFound(string name)
        {
            var lines = File.ReadAllLines(dishDbPath);
            bool found = false;

            for (int i = 1; i < lines.Length; i++)
            {
                var chunks = lines[i].Split('|');

                if (chunks.Length >= 0 && chunks[0].Trim().ToLower() == name.Trim().ToLower())
                {
                    found = true;
                    break;
                }
            }
            return found;
        }
    }


}