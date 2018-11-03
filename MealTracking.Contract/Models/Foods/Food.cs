using System.Collections.Generic;
using System.Linq;
using MealTracking.Contract.Extensions;
using MealTracking.Contract.Models.Base;

namespace MealTracking.Contract.Models.Foods
{
    public class Food : Entity
    {
        private IList<FoodUnit> _units = new List<FoodUnit>();
        
        public string Name { get; set; }

        public DefaultFoodUnit DefaultFoodUnit { get; set; } = new DefaultFoodUnit();

        public FoodUnit[] Units
        {
            get => _units.ToArray();
            set => _units = value;
        }  
        
        public void AddUnit(FoodUnit unit) => _units.Add(unit);

        public void RemoveUnit(FoodUnit unit) => _units.RemoveByReference(unit);

        public Food Clone() => new Food
        {
            Id = Id,
            Name = Name,
            DefaultFoodUnit = DefaultFoodUnit.Clone(),
            _units = _units.Select(unit => unit.Clone()).ToList()
        };

        public override string ToString() => Name;
    }
}
