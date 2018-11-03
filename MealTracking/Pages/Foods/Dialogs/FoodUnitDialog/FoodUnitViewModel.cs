using MealTracking.Constants;
using MealTracking.Contract.Models.Foods;
using MealTracking.Structures.ViewModels;
using MealTracking.Validation.Validators;

namespace MealTracking.Pages.Foods.Dialogs.FoodUnitDialog
{
    internal class FoodUnitViewModel : ViewModelBase
    {
        private readonly int _id;
        
        private string _name;
        private string _grams;

        private double _gramsValue;

        private FoodUnitViewModel(int id)
        {
            _id = id;
        }

        public string Name
        {
            get => _name;
            set
            {
                var validator = new TextLengthValidator(
                    minimum: FoodUnitConstants.MinimumAllowedNameLength,
                    maximum: FoodUnitConstants.MaximumAllowedNameLength
                );

                var validationResults = validator.Validate(value);
                SetValidationResults(validationResults);

                if (!SetField(ref _name, value) || validationResults != null) { }
            }
        }

        public string Grams
        {
            get => _grams;
            set
            {
                var validator = new DoubleValidator(minimum: FoodUnitConstants.MinimumAllowedUnitGrams);
                var validationResults = validator.Validate(value);
                SetValidationResults(validationResults);
                
                if (!SetField(ref _grams, value) || validationResults != null)
                {
                    return;
                }

                _gramsValue = double.Parse(_grams);
            }
        }

        public static FoodUnitViewModel FromModel(FoodUnit unit) => new FoodUnitViewModel(unit.Id)
        {
            Name = unit.Name,
            Grams = $"{unit.Grams}",
        };

        public FoodUnit ToModel() => new FoodUnit
        {
            Name = _name,
            Grams = _gramsValue,
            Id = _id,
        };
    }
}