using System.Linq;
using MealTracking.Contract.Models.Meals;
using MealTracking.Structures.Collections.Common;

namespace MealTracking.Structures.Collections
{
    internal class MealFoods
    {
        public ObservedCollection<MealFood> List { get; }

        public MealFoods(ObservedCollection<MealFood> list)
        {
            List = list;
        }
        
        public void Add(MealFood food)
        {
            var originalFood = List.FirstOrDefault(
                mealFood => mealFood.Food.Equals(food.Food)
                    && mealFood.FoodUnit.Equals(food.FoodUnit)
            );
            
            if (originalFood != null)
            {
                food.Amount += originalFood.Amount;
                
                List.Replace(originalFood, food);
            }
            else
            {
                List.Add(food);
            }
        }
        
        public void Edit(MealFood originalFood, MealFood newFood)
        {
            var existingFood = List.FirstOrDefault(
                mealFood => !ReferenceEquals(mealFood, originalFood)
                    && mealFood.Food.Equals(newFood.Food)
                    && mealFood.FoodUnit.Equals(newFood.FoodUnit)
            );
            
            if (existingFood != null)
            {
                newFood.Amount += existingFood.Amount;
                List.Remove(originalFood);
                List.Replace(existingFood, newFood);
            }
            else
            {
                List.Replace(originalFood, newFood);
            }
        }
    }
}