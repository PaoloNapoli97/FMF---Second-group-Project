using FileManager.Controller;
using static ResturantManagementLibrary.Dish;

namespace ResturantManagementLibrary
{
    class DishMenu
    {
        MainMenu mainMenu = new();
        DishFileManager dishFileManager = new();

        public void StartDishMenu()
        {
            List<Dish> dishes = dishFileManager.ReadDish();

            string[] options =
            {
                "1. Create a dish",
                "2. Shows all dishes",
                "3. Edit a dish",
                "4. Remove dish",
                "0. Back..."
            };
            int selectOption;

            Console.WriteLine($"Dish Menu:");


            do
            {
                MenuUtils.ShowMenuOption(options);
                selectOption = MenuUtils.ReadChoise();
                switch (selectOption)
                {
                    case 1: //? create a dish
                        Console.Clear();
                        CreateDishForm();
                        break;
                    case 2: //? print all dish
                        Console.Clear();
                        MenuUtils.ShowDishes(dishes);
                        StartDishMenu();
                        break;
                    case 3: //? edit a dish
                        MenuUtils.ShowDishes(dishes);
                        Console.WriteLine("Insert the name of the dish you wish to edit");
                        string tempName = Console.ReadLine();
                        if (dishFileManager.DishFound(tempName))
                        {
                            EditDishForm(tempName);
                        }
                        else
                        {
                            Console.WriteLine("ERROR: Name not contained \n");
                            mainMenu.StartMainMenu();
                        }
                        break;
                    case 4: //? delite a dish
                        Console.Clear();
                        Console.WriteLine($"Not implement...");
                        mainMenu.StartMainMenu();
                        break;
                    case 0: //? back to main menu
                        Console.Clear();
                        mainMenu.StartMainMenu();
                        break;
                    default:
                        Console.WriteLine($"Wrong option!");
                        break;
                }
            } while (
                        selectOption != 1 &&
                        selectOption != 2 &&
                        selectOption != 3 &&
                        selectOption != 4 &&
                        selectOption != 0
                    );
        }

        //? CREATE
        public void CreateDishForm()
        {
            Console.WriteLine($"Add a new dish:");

            Console.WriteLine($"Enter a name of dish");
            string name = Console.ReadLine();

            Console.WriteLine($"Enter a description of dish");
            string description = Console.ReadLine();

            string checkPrice = "Enter a price of dish";
            double price = DoubleControl(checkPrice); //TODO: manage exception

            CategoryList category = MenuUtils.ChoiseCategory();

            List<IngredientManager.Ingredient> selectedIngredients = ChoiseIngredients();

            dishFileManager.AddDish(name, description, price, category, selectedIngredients);

            //TODO: add 
            Console.WriteLine($"This {name} dish was created.");
            StartDishMenu();
        }

        public void PrintIngredient()
        {
            Console.WriteLine($"Ingredient list:");
            foreach (var ingredient in Enum.GetValues(typeof(IngredientManager.Ingredient)))
            {
                Console.WriteLine($"- {(int)ingredient}. {ingredient}");
            }
        }

        public List<IngredientManager.Ingredient> ChoiseIngredients()
        {
            List<IngredientManager.Ingredient> selectedIngredients = new();

            do
            {
                PrintIngredient();
                Console.WriteLine($"Choise ingredients for the dish (separated by comma):");
                string[] ingredientChoise = Console.ReadLine().Split(',');

                foreach (var choise in ingredientChoise)
                {
                    if (int.TryParse(choise, out int ingredientValue) && Enum.IsDefined(typeof(IngredientManager.Ingredient), ingredientValue))
                    {
                        selectedIngredients.Add((IngredientManager.Ingredient)ingredientValue);
                    }
                    else
                    {
                        Console.WriteLine($"Invalid ingredients: {choise}");

                    }
                }

            } while (selectedIngredients.Count == 0);

            return selectedIngredients;
        }

        // DA SPOSTARE NEGLI UTILS - CONTROLLO INPUT
        public double DoubleControl(string checkPrice)
        {
            string userInput;
            double number;

            do
            {
                Console.WriteLine(checkPrice);
                userInput = Console.ReadLine();
                if (!double.TryParse(userInput, out _))
                {
                    Console.WriteLine("Input Error, try again");
                }
            } while (!double.TryParse(userInput, out number));

            return number;
        }

        //? EDIT
        public void EditDishForm(string name)
        {
            string[] options =
            {
                "1 - Chage name",
                "2 - Change description",
                "3 - Change Price",
                "4 - Change type of category",
                "5 - Change ingredients (separated by comma)",
                "0 - Exit"
            };
            string newValue;

            int selectOption;

            do
            {
                Console.WriteLine($"Insert new data for the dish: {name}");
                MenuUtils.ShowMenuOption(options);
                selectOption = MenuUtils.ReadChoise();
                switch (selectOption)
                {
                    case 1:
                        Console.WriteLine("Insert the new name: ");
                        newValue = Console.ReadLine();
                        dishFileManager.EditDishDB(name, selectOption, newValue);
                        EditDishForm(newValue);
                        break;

                    case 2:
                        Console.WriteLine("Insert a new description");
                        newValue = Console.ReadLine();
                        dishFileManager.EditDishDB(name, selectOption, newValue);
                        break;

                    case 3:
                        newValue = "Insert the new price";
                        double price = DoubleControl(newValue);
                        dishFileManager.EditDishDB(name, selectOption, price);
                        break;

                    case 4:
                        Console.WriteLine("Insert a new category");
                        CategoryList categoryList = MenuUtils.ChoiseCategory();
                        dishFileManager.EditDishDB(name, selectOption, categoryList);
                        break;

                    case 5:
                        Console.WriteLine("Insert new ingredients (separated by ';')");
                        PrintIngredient();
                        newValue = Console.ReadLine();
                        dishFileManager.EditDishDB(name, selectOption, newValue);
                        break;

                    case 0: //? back to main menu
                        mainMenu.StartMainMenu();
                        break;

                    default:
                        Console.WriteLine($"Wrong option!");
                        break;
                }
            } while (
                    selectOption != 0
                    );

        }

        //? DELITE

    }
}