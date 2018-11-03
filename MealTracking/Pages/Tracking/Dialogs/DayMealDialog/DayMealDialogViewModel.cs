using System.Windows.Input;
using MealTracking.Contract.Extensions;
using MealTracking.Contract.Models.Meals;
using MealTracking.Pages.Meals.Dialogs.MealFoodDialog;
using MealTracking.Pages.Tracking.Dialogs.MealTemplateDialog;
using MealTracking.Structures;
using MealTracking.Structures.Dialogs;

namespace MealTracking.Pages.Tracking.Dialogs.DayMealDialog
{
    internal class DayMealDialogViewModel : DialogModelBase
    {
        public static string DialogsIdentifier => "DayMealDialogDialogsIdentifier";
        
        private readonly DialogFactory _dialogs;
        
        public DayMealViewModel DayMeal { get; set; }

        #region Commands

        public ICommand OpenAddMealFoodDialogCommand { get; }

        public ICommand OpenEditMealFoodDialogCommand { get; }
        
        public ICommand RemoveMealFoodCommand { get; }

        public ICommand OpenMealTemplateDialogCommand { get; }
        
        #endregion

        #region MealFoodDialog

        private async void OpenAddMealFoodDialogAsync()
        {
            var food = new MealFood
            {
                Amount = 1d
            };
                
            var dialog = _dialogs.For<MealFoodDialogViewModel>(DialogsIdentifier);
            dialog.Data.DialogTitle = "New meal food";
            dialog.Data.SubmitButtonTitle = "Add";
            dialog.Data.MealFood = MealFoodViewModel.FromModel(food);
                
            var dialogResult = await dialog.Show();

            if (dialogResult != DialogResult.Ok)
            {
                return;
            }

            food = dialog.Data.MealFood.ToModel();
            DayMeal.Foods.Add(food);
        }
        
        private async void OpenEditMealFoodDialogAsync(MealFood food)
        {
            var foodClone = food.Clone();
                
            var dialog = _dialogs.For<MealFoodDialogViewModel>(DialogsIdentifier);
            dialog.Data.DialogTitle = "Modified meal food";
            dialog.Data.SubmitButtonTitle = "Save";
            dialog.Data.MealFood = MealFoodViewModel.FromModel(foodClone);
                
            var dialogResult = await dialog.Show();

            if (dialogResult != DialogResult.Ok)
            {
                return;
            }

            foodClone = dialog.Data.MealFood.ToModel();
            DayMeal.Foods.Edit(food, foodClone);
        }

        #endregion

        #region MealTemplateDialog

        private async void OpenMealTemplateDialogAsync()
        {
            var dialog = _dialogs.For<MealTemplateDialogViewModel>(DialogsIdentifier);
            dialog.Data.DialogTitle = "Select template";
            dialog.Data.SubmitButtonTitle = "Select";
            
            var dialogResult = await dialog.Show();

            if (dialogResult != DialogResult.Ok)
            {
                return;
            }

            var meal = dialog.Data.MealTemplate;
            
            foreach (var food in meal.Foods)
            {
                DayMeal.Foods.Add(food.Clone());
            }

            if (string.IsNullOrWhiteSpace(DayMeal.Name))
            {
                DayMeal.Name = meal.Name;
            }
        }

        #endregion
        
        public DayMealDialogViewModel(DialogFactory dialogs)
        {
            _dialogs = dialogs;
            
            OpenAddMealFoodDialogCommand = new Command(OpenAddMealFoodDialogAsync);
            
            OpenEditMealFoodDialogCommand = new Command<MealFood>(OpenEditMealFoodDialogAsync);
            
            RemoveMealFoodCommand = new Command<MealFood>(food => DayMeal.Foods.List.RemoveByReference(food));
            
            OpenMealTemplateDialogCommand = new Command(OpenMealTemplateDialogAsync);
        }
    }
}