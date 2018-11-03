using MealTracking.Contract.Models.Foods;

namespace MealTracking.Contract.Models.Meals
{
    public class MealFood
    {
        public Food Food { get; set; }

        public FoodUnit FoodUnit { get; set; }

        public double Amount { get; set; }

        public Nutrients Nutrients => Food.DefaultFoodUnit.NutrientsPer1G * FoodUnit.Grams * Amount;
        
        public MealFood Clone() => new MealFood
        {
            Amount = Amount,
            Food = Food.Clone(),
            FoodUnit = FoodUnit.Clone()
        };
    }
}
