namespace ResturantManagementLibrary
{
    public class Dish
    {
        public enum CategoryList
        {
            NotCategory,
            Appetizer,
            FirstDish,
            SecondDish,
            SingleDish,
            Dessert
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public double Price; //TODO: to private

        public CategoryList Category { get; set; }
        public List<IngredientManager.Ingredient> Ingredients;

        public Dish(string name, string description, double price, CategoryList category, List<IngredientManager.Ingredient> ingredients)
        {
            Name = name;
            Description = description;
            Price = price;
            Category = category;
            Ingredients = ingredients;
        }
    }
}