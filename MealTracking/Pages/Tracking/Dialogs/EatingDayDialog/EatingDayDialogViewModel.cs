using System;
using System.Windows.Input;
using MealTracking.Contract.Extensions;
using MealTracking.Contract.Models.Days;
using MealTracking.Pages.Tracking.Dialogs.DayMealDialog;
using MealTracking.Structures;
using MealTracking.Structures.Dialogs;

namespace MealTracking.Pages.Tracking.Dialogs.EatingDayDialog
{
    internal class EatingDayDialogViewModel : DialogModelBase
    {
        public static string DialogsIdentifier => "EatingDayDialogDialogsIdentifier";

        private readonly DialogFactory _dialogs;
        
        public EatingDayViewModel EatingDay { get; set; }

        #region Commands

        public ICommand OpenAddDayMealDialogCommand { get; }
        
        public ICommand OpenEditDayMealDialogCommand { get; }
        
        public ICommand RemoveDayMealCommand { get; }
        
        #endregion

        #region DayMealDialog

        private async void OpenAddDayMealDialogAsync()
        {
            var meal = new DayMeal
            {
                Hour = DateTime.Today.Hour 
            };
            
            var dialog = _dialogs.For<DayMealDialogViewModel>(DialogsIdentifier);
            dialog.Data.DialogTitle = "New meal";
            dialog.Data.SubmitButtonTitle = "Create";
            dialog.Data.DayMeal = DayMealViewModel.FromModel(meal);
            
            var dialogResult = await dialog.Show();

            if (dialogResult != DialogResult.Ok)
            {
                return;
            }

            meal = dialog.Data.DayMeal.ToModel();
            EatingDay.Meals.Add(meal);
            EatingDay.Meals.Refresh(meal);
        }

        private async void OpenEditDayMealDialogAsync(DayMeal meal)
        {
            var mealClone = meal.Clone();
            
            var dialog = _dialogs.For<DayMealDialogViewModel>(DialogsIdentifier);
            dialog.Data.DialogTitle = "Modified meal";
            dialog.Data.SubmitButtonTitle = "Save";
            dialog.Data.DayMeal = DayMealViewModel.FromModel(mealClone);
            
            var dialogResult = await dialog.Show();

            if (dialogResult != DialogResult.Ok)
            {
                return;
            }

            mealClone = dialog.Data.DayMeal.ToModel();
            EatingDay.Meals.Replace(meal, mealClone);
        }
        
        #endregion

        private void RemoveMeal(DayMeal meal) => EatingDay.Meals.RemoveByReference(meal);

        public EatingDayDialogViewModel(DialogFactory dialogs)
        {
            _dialogs = dialogs;
            
            OpenAddDayMealDialogCommand = new Command(OpenAddDayMealDialogAsync);
            
            OpenEditDayMealDialogCommand = new Command<DayMeal>(OpenEditDayMealDialogAsync);
            
            RemoveDayMealCommand = new Command<DayMeal>(RemoveMeal);
        }
    }
}