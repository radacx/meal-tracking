using System.Windows.Input;
using MealTracking.Contract.Extensions;
using MealTracking.Contract.Models.Meals;
using MealTracking.Pages.Meals.Dialogs.MealFoodDialog;
using MealTracking.Structures;
using MealTracking.Structures.Dialogs;

namespace MealTracking.Pages.Meals.Dialogs.MealDialog
{
    internal class MealDialogViewModel : DialogModelBase
    {
        private readonly DialogFactory _dialogs;

        public static string DialogsIdentifier => "MealDialogDialogsIdentifier";
        
        public MealViewModel Meal { get; set; }

        #region Commands

        public ICommand OpenAddMealFoodDialogCommand { get; }
        
        public ICommand OpenEditMealFoodDialogCommand { get; }
        
        public ICommand RemoveMealFood { get; }

        #endregion

        #region MealFoodDialog

        private async void OpenAddMealFoodDialogAsync()
        {
            var mealFood = new MealFood
            {
                Amount = 1d
            };
            
            var dialog = _dialogs.For<MealFoodDialogViewModel>(DialogsIdentifier);
            dialog.Data.DialogTitle = "New meal food";
            dialog.Data.SubmitButtonTitle = "Add";
            dialog.Data.MealFood = MealFoodViewModel.FromModel(mealFood);
                
            var dialogResult = await dialog.Show();

            if (dialogResult != DialogResult.Ok)
            {
                return;
            }

            mealFood = dialog.Data.MealFood.ToModel();
            Meal.Foods.Add(mealFood);
        }

        private async void OpenEditMealFoodDialogAsync(MealFood mealFood)
        {
            var mealFoodClone = mealFood.Clone();
            
            var dialog = _dialogs.For<MealFoodDialogViewModel>(DialogsIdentifier);
            dialog.Data.DialogTitle = "Modified meal food";
            dialog.Data.SubmitButtonTitle = "Save";
            dialog.Data.MealFood = MealFoodViewModel.FromModel(mealFoodClone);
                
            var dialogResult = await dialog.Show();

            if (dialogResult != DialogResult.Ok)
            {
                return;
            }

            mealFoodClone = dialog.Data.MealFood.ToModel();
            Meal.Foods.Edit(mealFood, mealFoodClone);
        }

        #endregion

        public MealDialogViewModel(DialogFactory dialogs)
        {
            _dialogs = dialogs;
            
            OpenAddMealFoodDialogCommand = new Command(OpenAddMealFoodDialogAsync);
            
            OpenEditMealFoodDialogCommand = new Command<MealFood>(OpenEditMealFoodDialogAsync);
            
            RemoveMealFood = new Command<MealFood>(food => Meal.Foods.List.RemoveByReference(food));
        }
    }
}