using System;
using System.Collections.Generic;
using System.Linq;
using MealTracking.Contract.Extensions;
using MealTracking.Contract.Models.Base;
using MealTracking.Contract.Models.Foods;

namespace MealTracking.Contract.Models.Days
{
    public class EatingDay : Entity
    {
        private IList<DayMeal> _meals = new List<DayMeal>();
     
        public DateTime Date { get; set; }
        
        public double Bodyweight { get; set; }
        
        public DayMeal[] Meals
        {
            get => _meals.ToArray();
            set => _meals = value;
        }

        public void AddMeal(DayMeal meal) => _meals.Add(meal);

        public void RemoveMeal(DayMeal meal) => _meals.RemoveByReference(meal);

        public Nutrients Nutrients
            => Meals.Aggregate(
                new Nutrients(),
                (total, meal) => total + meal.Nutrients
            );
        
        public EatingDay Clone() => new EatingDay
        {
            Id = Id,
            Date = Date,
            Bodyweight = Bodyweight,
            _meals = _meals.Select(meal => meal.Clone()).ToList(),
        };
    }
}