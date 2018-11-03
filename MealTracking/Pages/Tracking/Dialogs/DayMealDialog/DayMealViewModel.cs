using System.Linq;
using MealTracking.Constants;
using MealTracking.Contract.Models.Days;
using MealTracking.Contract.Models.Meals;
using MealTracking.Structures.Collections;
using MealTracking.Structures.Collections.Common;
using MealTracking.Structures.ViewModels;
using MealTracking.Validation.Validators;

namespace MealTracking.Pages.Tracking.Dialogs.DayMealDialog
{
    internal class DayMealViewModel : ViewModelBase
    {
        private readonly int _id;
        
        private string _name;

        private DayMealViewModel(int id)
        {
            _id = id;
        }

        public string Name
        {
            get => _name;
            set
            {
                var validator = new TextLengthValidator(
                    minimum: DayMealConstants.MinimumAllowedNameLength,
                    maximum: DayMealConstants.MaximumAllowedNameLength
                );

                var validationResults = validator.Validate(value);
                SetValidationResults(validationResults);
                
                if (!SetField(ref _name, value) || validationResults != null) {}
            }
        }

        public int Hour { get; set; }

        public MealFoods Foods { get; set; }

        public static DayMealViewModel FromModel(DayMeal meal) => new DayMealViewModel(meal.Id)
        {
            Name = meal.Name,
            Hour = meal.Hour,
            Foods = new MealFoods(new ObservedCollection<MealFood>(meal.Foods, meal.AddFood, meal.RemoveFood)),
        };

        public DayMeal ToModel() => new DayMeal
        {
            Hour = Hour,
            Name = Name,
            Foods = Foods.List.ToArray(),
            Id = _id,
        };
    }
}