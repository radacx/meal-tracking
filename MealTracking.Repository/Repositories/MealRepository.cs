using System.Collections.Generic;
using System.Linq;
using MealTracking.Contract.Models.Meals;
using MealTracking.Contract.Repositories;

namespace MealTracking.Repository.Repositories
{
    public class MealRepository : Repository<MealTemplate>
    {
        public MealRepository(DatabaseConfiguration configuration) : base(configuration) { }

        public override IEnumerable<MealTemplate> GetAll()
            => Execute(
                collection => collection
                    .Include(x => x.Foods[0].Food)
                    .Include(x => x.Foods[0].Food.Units)
                    .Include(x => x.Foods[0].Food.DefaultFoodUnit.FoodUnit)
                    .Include(x => x.Foods[0].FoodUnit)
                    .FindAll()
                    .ToArray()
            );
    }
}