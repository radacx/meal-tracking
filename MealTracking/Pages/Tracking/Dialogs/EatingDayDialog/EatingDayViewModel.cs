using System;
using System.Linq;
using MealTracking.Contract.Models.Days;
using MealTracking.Structures.Collections.Common;
using MealTracking.Structures.ViewModels;
using MealTracking.Validation.Validators;

namespace MealTracking.Pages.Tracking.Dialogs.EatingDayDialog
{
    internal class EatingDayViewModel : ViewModelBase
    {
        private readonly int _id;
        
        private string _bodyweight;

        private double _bodyweightValue;

        private EatingDayViewModel(int id)
        {
            _id = id;
        }

        public DateTime Date { get; set; }

        public string Bodyweight
        {
            get => _bodyweight;
            set
            {
                var validator = new DoubleValidator(minimum: 0);
                var validationResults = validator.Validate(value);
                SetValidationResults(validationResults);
                
                if (!SetField(ref _bodyweight, value) || validationResults != null)
                {
                    return;
                }

                _bodyweightValue = double.Parse(value);
            }
        }

        public ObservedCollection<DayMeal> Meals { get; set; }
        
        public static EatingDayViewModel FromModel(EatingDay day) => new EatingDayViewModel(day.Id)
        {
            Date = day.Date,
            Meals = new ObservedCollection<DayMeal>(day.Meals, day.AddMeal, day.RemoveMeal),
            Bodyweight = $"{day.Bodyweight}",
        };

        public EatingDay ToModel() => new EatingDay
        {
            Date = Date,
            Meals = Meals.ToArray(),
            Bodyweight = _bodyweightValue,
            Id = _id,
        };
    }
}