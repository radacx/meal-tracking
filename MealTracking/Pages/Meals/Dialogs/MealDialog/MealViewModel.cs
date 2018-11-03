using System.Linq;
using MealTracking.Constants;
using MealTracking.Contract.Models.Meals;
using MealTracking.Structures.Collections;
using MealTracking.Structures.Collections.Common;
using MealTracking.Structures.ViewModels;
using MealTracking.Validation.Validators;

namespace MealTracking.Pages.Meals.Dialogs.MealDialog
{
    internal class MealViewModel : ViewModelBase
    {
        private readonly int _id;
        
        private string _name;

        private MealViewModel(int id)
        {
            _id = id;
        }

        public string Name
        {
            get => _name;
            set
            {
                var validator = new TextLengthValidator(
                    minimum: MealConstants.MinimumAllowedNameLength,
                    maximum: MealConstants.MaximumAllowedNameLength
                );

                var validationResults = validator.Validate(value);
                SetValidationResults(validationResults);
                
                if (!SetField(ref _name, value) || validationResults != null) { }
            }
        }

        public MealFoods Foods { get; set; }
        
        public static MealViewModel FromModel(MealTemplate meal) => new MealViewModel(meal.Id)
        {
            Name = meal.Name,
            Foods = new MealFoods(new ObservedCollection<MealFood>(meal.Foods, meal.AddFood, meal.RemoveFood)),
        };

        public MealTemplate ToModel() => new MealTemplate
        {
            Name = _name,
            Foods = Foods.List.ToArray(),
            Id = _id,
        };
    }
}