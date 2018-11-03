namespace MealTracking.Contract.Models.Foods
{
    public class DefaultFoodUnit
    {
        public Nutrients Nutrients { get; set; } = new Nutrients();

        public FoodUnit FoodUnit { get; set; } = new FoodUnit();
        
        public Nutrients NutrientsPer1G => new Nutrients
        {
            Macros = Nutrients.Macros / FoodUnit.Grams,
            Micros = Nutrients.Micros / FoodUnit.Grams
        };
        
        public Nutrients NutrientsPer100G => NutrientsPer1G * 100;
        
        public DefaultFoodUnit Clone() => new DefaultFoodUnit
        {
            Nutrients = Nutrients.Clone(),
            FoodUnit = FoodUnit.Clone(),
        };
    }
}