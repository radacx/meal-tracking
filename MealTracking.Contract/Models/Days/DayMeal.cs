using System.Collections.Generic;
using System.Linq;
using MealTracking.Contract.Extensions;
using MealTracking.Contract.Models.Base;
using MealTracking.Contract.Models.Foods;
using MealTracking.Contract.Models.Meals;

namespace MealTracking.Contract.Models.Days
{
    public class DayMeal : Entity
    {
        public string Name { get; set; }
        
        public int Hour { get; set; }
        
        private IList<MealFood> _foods = new List<MealFood>();
        
        public MealFood[] Foods
        {
            get => _foods.ToArray();
            set => _foods = value;
        }

        public void AddFood(MealFood food) => _foods.Add(food);
        
        public void RemoveFood(MealFood food) => _foods.RemoveByReference(food);

        public Nutrients Nutrients
            => Foods.Aggregate(
                new Nutrients(),
                (total, foodWithAmount) => total + foodWithAmount.Nutrients
            );
        
        public DayMeal Clone() => new DayMeal
        {
            Id = Id,
            Name = Name,
            Hour = Hour,
            _foods = _foods.Select(x => x.Clone()).ToList()
        };
    }
}