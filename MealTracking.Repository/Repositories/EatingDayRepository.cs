using System.Collections.Generic;
using System.Linq;
using MealTracking.Contract.Models.Days;
using MealTracking.Contract.Repositories;

namespace MealTracking.Repository.Repositories
{
    public class EatingDayRepository : Repository<EatingDay>
    {
        public EatingDayRepository(DatabaseConfiguration configuration) : base(configuration) { }

        public override IEnumerable<EatingDay> GetAll()
            => Execute(
                collection => collection
                    .Include(x => x.Meals[0].Foods[0].Food)
                    .Include(x => x.Meals[0].Foods[0].Food.Units)
                    .Include(x => x.Meals[0].Foods[0].Food.DefaultFoodUnit.FoodUnit)
                    .Include(x => x.Meals[0].Foods[0].FoodUnit)
                    .FindAll()
                    .ToArray()
            );
    }
}