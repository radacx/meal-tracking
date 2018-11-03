using System.Collections.Generic;
using System.Linq;
using MealTracking.Contract.Models.Foods;
using MealTracking.Contract.Repositories;

namespace MealTracking.Repository.Repositories
{
    public class FoodRepository : Repository<Food>
    {
        public FoodRepository(DatabaseConfiguration configuration) : base(configuration) { }

        public override IEnumerable<Food> GetAll()
            => Execute(
                collection => collection.Include(x => x.DefaultFoodUnit.FoodUnit).Include(x => x.Units).FindAll().ToArray()
            );
    }
}