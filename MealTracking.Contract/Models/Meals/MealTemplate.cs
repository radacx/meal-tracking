using System.Collections.Generic;
using System.Linq;
using MealTracking.Contract.Extensions;
using MealTracking.Contract.Models.Base;
using MealTracking.Contract.Models.Foods;

namespace MealTracking.Contract.Models.Meals
{
    public class MealTemplate : Entity
    {
        public string Name { get; set; }

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
        
        public MealTemplate Clone() => new MealTemplate
        {
            Id = Id,
            Name = Name,
            _foods = _foods.Select(food => food.Clone()).ToList()
        };

        public override string ToString() => Name;
    }
}
