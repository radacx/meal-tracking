using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using MealTracking.Constants;
using MealTracking.Contract.Models.Foods;
using MealTracking.Structures.ViewModels;
using MealTracking.Validation.Validators;

namespace MealTracking.Pages.Foods.Controls
{
    internal class DefaultFoodUnitViewModel : ViewModelBase
    {
        private int _id;
        
        private string _protein = string.Empty;
        private string _carbs = string.Empty;
        private string _fats = string.Empty;
        private string _alcohol = string.Empty;
        private string _grams = string.Empty;
        private string _name = string.Empty;

        private double _proteinValue;
        private double _carbsValue;
        private double _fatsValue;
        private double _alcoholValue;
        private double _gramsValue;

        private void ValidateUnitGrams() => ValidateUnitGrams(_grams, nameof(Grams));
        
        private IEnumerable<string> ValidateUnitGrams(string grams, [CallerMemberName] string propertyName = "")
        {
            var validator = new DoubleValidator(minimum: MinimumGrams);
            var validationResults = validator.Validate(grams);
            
            SetValidationResults(validationResults, propertyName);
            
            return validationResults;
        }

        private IEnumerable<string> ValidateMacroGrams(string grams, [CallerMemberName] string propertyName = "")
        {
            var validator = new DoubleValidator(minimum: FoodUnitConstants.MinimumAllowedMacroGrams);
            var validationResults = validator.Validate(grams);
            
            SetValidationResults(validationResults, propertyName);
            
            return validationResults;
        }

        private IEnumerable<string> ValidateName(string name, [CallerMemberName] string propertyName = "")
        {
            var validator = new TextLengthValidator(
                minimum: FoodUnitConstants.MinimumAllowedNameLength,
                maximum: FoodUnitConstants.MaximumAllowedNameLength
            );
            
            var validationResults = validator.Validate(name);
            
            SetValidationResults(validationResults, propertyName);
            
            return validationResults;
        }
        
        public string Grams
        {
            get => _grams;
            set
            {
                var validationResults = ValidateUnitGrams(value);
                
                if (!SetField(ref _grams, value) || validationResults != null)
                {
                    return;
                }

                _gramsValue = double.Parse(value);
                OnPropertyChanged(nameof(TotalCalories));
            }
        }
        
        public string Protein
        {
            get => _protein;
            set
            {
                var validationResults = ValidateMacroGrams(value);
                
                if (!SetField(ref _protein, value) || validationResults != null)
                {
                    return;
                }

                _proteinValue = double.Parse(value);
                OnPropertyChanged(nameof(TotalCalories));
                ValidateUnitGrams();
            }
        }

        public string Carbs
        {
            get => _carbs;
            set
            {
                var validationResults = ValidateMacroGrams(value);
                
                if (!SetField(ref _carbs, value) || validationResults != null)
                {
                    return;
                }

                _carbsValue = double.Parse(value);
                OnPropertyChanged(nameof(TotalCalories));
                ValidateUnitGrams();
            }
        }

        public string Fats
        {
            get => _fats;
            set
            {
                var validationResults = ValidateMacroGrams(value);
                
                if (!SetField(ref _fats, value) || validationResults != null)
                {
                    return;
                }

                _fatsValue = double.Parse(value);
                OnPropertyChanged(nameof(TotalCalories));
                ValidateUnitGrams();
            }
        }
        
        public string Alcohol
        {
            get => _alcohol;
            set
            {
                var validationResults = ValidateMacroGrams(value);
                
                if (!SetField(ref _alcohol, value) || validationResults != null)
                {
                    return;
                }

                _alcoholValue = double.Parse(value);
                OnPropertyChanged(nameof(TotalCalories));
                ValidateUnitGrams();
            }
        }

        public double TotalCalories => new Macros(_proteinValue, _carbsValue, _fatsValue, _alcoholValue).Calories / _gramsValue * 100;

        private double MinimumGrams => Math.Max(FoodUnitConstants.MinimumAllowedUnitGrams, TotalMacroGrams);
        
        private double TotalMacroGrams => _proteinValue + _carbsValue + _fatsValue;

        public string Name
        {
            get => _name;
            set
            {
                var validationResults = ValidateName(value);
                
                if (!SetField(ref _name, value) || validationResults != null) { }
            }
        }

        private DefaultFoodUnitViewModel()
        {
            PropertyChanged += OnPropertyChanged;
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(TotalCalories))
            {
                var validationResults = TotalCalories <= 0 ? new[] { "Food should have more than 0 kcal." } : null;
                SetValidationResults(validationResults, nameof(TotalCalories));
            }
        }

        public static DefaultFoodUnitViewModel FromModel(Contract.Models.Foods.DefaultFoodUnit foodUnit) => new DefaultFoodUnitViewModel
        {
            Fats = $"{foodUnit.Nutrients.Macros.Fats}",
            Protein = $"{foodUnit.Nutrients.Macros.Protein}",
            Carbs = $"{foodUnit.Nutrients.Macros.Carbs}",
            Alcohol = $"{foodUnit.Nutrients.Macros.Alcohol}",
            Grams = $"{foodUnit.FoodUnit.Grams}",
            Name = foodUnit.FoodUnit.Name,
            _id = foodUnit.FoodUnit.Id,
        };

        public Contract.Models.Foods.DefaultFoodUnit ToModel() => new Contract.Models.Foods.DefaultFoodUnit
        {
            FoodUnit = new FoodUnit
            {
                Grams = _gramsValue,
                Name = _name,
                Id = _id,
            },
            Nutrients = new Nutrients
            {
                Macros = new Macros
                {
                    Protein = _proteinValue,
                    Carbs = _carbsValue,
                    Fats = _fatsValue,
                    Alcohol = _alcoholValue
                }
            }
        };
    }
}