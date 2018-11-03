using System.Collections.Generic;
using System.Linq;
using MealTracking.Contract.Models.Foods;
using MealTracking.Repository;
using MealTracking.Service.Common;

namespace MealTracking.Service.Services
{
    public class FoodService : ServiceBase<Food>
    {
        private readonly Repository<FoodUnit> _foodUnitRepository;
        
        public FoodService(Repository<Food> repository, Repository<FoodUnit> foodUnitRepository) : base(repository)
        {
            _foodUnitRepository = foodUnitRepository;
        }

        public IEnumerable<Food> GetAll() => Repository.GetAll();

        public void Create(Food food)
        {
            _foodUnitRepository.CreateRange(food.Units);
            _foodUnitRepository.Create(food.DefaultFoodUnit.FoodUnit);
            
            Repository.Create(food);
        }

        public void Update(Food food, Food originalFood = null)
        {
            var newUnits = food.Units.Where(x => x.Id == 0);
            var unitsToUpdate = food.Units.Where(x => x.Id != 0).ToHashSet();

            if (originalFood == null)
            {
                originalFood = Repository.Get(food.Id);
            }

            var oldUnits = originalFood.Units.Except(unitsToUpdate); 
            
            _foodUnitRepository.DeleteRange(oldUnits);
            _foodUnitRepository.UpdateRange(unitsToUpdate);
            _foodUnitRepository.CreateRange(newUnits);

            _foodUnitRepository.Update(food.DefaultFoodUnit.FoodUnit);
            
            Repository.Update(food);
        }

        public void Delete(Food food)
        {
            _foodUnitRepository.DeleteRange(food.Units);
            _foodUnitRepository.Delete(food.DefaultFoodUnit.FoodUnit);
            
            Repository.Delete(food);
        }
    }
}