using System.Linq;
using MealTracking.Constants;
using MealTracking.Contract.Models.Foods;
using MealTracking.Pages.Foods.Controls;
using MealTracking.Structures.Collections.Common;
using MealTracking.Structures.ViewModels;
using MealTracking.Validation.Validators;
using DefaultFoodUnit = MealTracking.Contract.Models.Foods.DefaultFoodUnit;

namespace MealTracking.Pages.Foods.Dialogs.FoodDialog
{
    internal class FoodUnitWithNutrientsValue
    {
        public FoodUnit FoodUnit { get; }

        public DefaultFoodUnit DefaultFoodUnit { get; set; }

        public Nutrients Nutrients => DefaultFoodUnit.NutrientsPer1G * FoodUnit.Grams;
        
        public FoodUnitWithNutrientsValue(FoodUnit foodUnit, DefaultFoodUnit defaultFoodUnit)
        {
            FoodUnit = foodUnit;
            DefaultFoodUnit = defaultFoodUnit;
        }
    }
    
    
    internal class FoodViewModel : ViewModelBase
    {
        private readonly int _id;
        
        private string _name;

        private FoodViewModel(int id)
        {
            _id = id;
        }

        public string Name
        {
            get => _name;
            set
            {
                var validator = new TextLengthValidator(
                    minimum: FoodConstants.MinimumAllowedNameLength,
                    maximum: FoodConstants.MaximumAllowedNameLength
                );
                
                var validationResults = validator.Validate(value);
                SetValidationResults(validationResults);
                
                if (!SetField(ref _name, value) || validationResults != null) { }
            }
        }

        public DefaultFoodUnitViewModel DefaultFoodUnit { get; set; }
        
        public ObservedCollection<FoodUnit> Units { get; private set; }

        public WpfObservableRangeCollection<FoodUnitWithNutrientsValue> UnitsWithNutrients { get; set; }

        public static FoodViewModel FromModel(Food food)
        {
            var defaultFoodUnitViewModel = DefaultFoodUnitViewModel.FromModel(food.DefaultFoodUnit);

            var units = new ObservedCollection<FoodUnit>(
                food.Units,
                food.AddUnit,
                food.RemoveUnit
            );
            
            defaultFoodUnitViewModel.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(defaultFoodUnitViewModel.TotalCalories))
                {
                    units.Refresh();
                }
            };

            var foodViewModel = new FoodViewModel(food.Id)
            {
                Name = food.Name,
                DefaultFoodUnit = defaultFoodUnitViewModel,
                Units = units,
                UnitsWithNutrients = new WpfObservableRangeCollection<FoodUnitWithNutrientsValue>(
                    units.Select(
                        foodUnit => new FoodUnitWithNutrientsValue(foodUnit, defaultFoodUnitViewModel.ToModel())
                    )
                ),
            };
            
            units.CollectionChanged += (sender, args) =>
            {
                foodViewModel.UnitsWithNutrients.Clear();
                
                var updatedUnits = units.Select(
                        foodUnit => new FoodUnitWithNutrientsValue(foodUnit, defaultFoodUnitViewModel.ToModel())
                    )
                    .ToArray();
                
                foodViewModel.UnitsWithNutrients.AddRange(updatedUnits);
            };
            
            return foodViewModel;
        }

        public Food ToModel() => new Food
        {
            DefaultFoodUnit = DefaultFoodUnit.ToModel(),
            Name = Name,
            Units = Units.ToArray(),
            Id = _id,
        };
    }
}