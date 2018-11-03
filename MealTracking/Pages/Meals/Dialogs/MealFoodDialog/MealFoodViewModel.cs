using MealTracking.Constants;
using MealTracking.Contract.Models.Foods;
using MealTracking.Contract.Models.Meals;
using MealTracking.Structures.ViewModels;
using MealTracking.Validation.Validators;

namespace MealTracking.Pages.Meals.Dialogs.MealFoodDialog
{
    internal class MealFoodViewModel : ViewModelBase
    {
        private Food _selectedFood;
        private FoodUnit _selectedFoodUnit;
        private string _amount;

        private double _amountValue;
        
        public Food Food
        {
            get => _selectedFood;
            set
            {
                var validationResults = value == null ? new[] { "Select a food." } : null;
                SetValidationResults(validationResults);

                if (!SetField(ref _selectedFood, value) || validationResults != null) { }
            }
        }

        public FoodUnit FoodUnit
        {
            get => _selectedFoodUnit;
            set
            {
                var validationResults = value == null ? new[] { "Select a unit." } : null;
                SetValidationResults(validationResults);

                if (!SetField(ref _selectedFoodUnit, value) || validationResults != null) { }
            }
        }

        public string Amount
        {
            get => _amount;
            set
            {
                var validator = new DoubleValidator(minimum: MealFoodConstants.MinimumAmount);
                var validationResults = validator.Validate(value);
                SetValidationResults(validationResults);
                
                if (!SetField(ref _amount, value) || validationResults != null)
                {
                    return;
                }

                _amountValue = double.Parse(_amount);
            }
        }
        
        public static MealFoodViewModel FromModel(MealFood food) => new MealFoodViewModel
        {
            Food = food.Food,
            FoodUnit = food.FoodUnit,
            Amount = $"{food.Amount}"
        };

        public MealFood ToModel() => new MealFood
        {
            Food = Food,
            FoodUnit = FoodUnit,
            Amount = _amountValue
        };
    }
}