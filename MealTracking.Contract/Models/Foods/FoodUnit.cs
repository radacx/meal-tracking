using MealTracking.Contract.Models.Base;

namespace MealTracking.Contract.Models.Foods
{
    public class FoodUnit : Entity
    {
        public string Name { get; set; }

        public double Grams { get; set; }
        
        public FoodUnit Clone() => new FoodUnit
        {
            Id = Id,
            Name = Name,
            Grams = Grams
        };
        
        public override string ToString() => Name;
    }
}
