using System.Windows.Input;
using MealTracking.Constants;
using MealTracking.Contract.Extensions;
using MealTracking.Contract.Models.Foods;
using MealTracking.Pages.Foods.Dialogs.FoodUnitDialog;
using MealTracking.Structures;
using MealTracking.Structures.Dialogs;

namespace MealTracking.Pages.Foods.Dialogs.FoodDialog
{
    internal class FoodDialogViewModel : DialogModelBase
    {
        private readonly DialogFactory _dialogs;
        
        public FoodViewModel Food { get; set; }

        #region  Commands

        public ICommand OpenAddUnitDialogCommand { get; }
        
        public ICommand OpenEditUnitDialogCommand { get; }
        
        public ICommand RemoveUnitCommand { get; }

        #endregion
        
        public static string FoodDialogDialogsIdentifier => "FoodDialogDialogsIdentifier";

        private void RemoveUnit(FoodUnit unit) => Food.Units.RemoveByReference(unit);

        #region FoodUnit dialog
        
        private async void OpenAddUnitDialogAsync()
        {
            var unit = new FoodUnit
            {
                Grams = FoodUnitConstants.MinimumAllowedUnitGrams
            };
            
            var dialog = _dialogs.For<FoodUnitDialogViewModel>(FoodDialogDialogsIdentifier);
            dialog.Data.FoodUnit = FoodUnitViewModel.FromModel(unit);
            dialog.Data.DialogTitle = "New food unit";
            dialog.Data.SubmitButtonTitle = "Create";

            var dialogResult = await dialog.Show();

            if (dialogResult != DialogResult.Ok)
            {
                return;
            }

            unit = dialog.Data.FoodUnit.ToModel();
            Food.Units.Add(unit);
        }

        private async void OpenEditUnitDialogAsync(FoodUnit unit)
        {
            var unitClone = unit.Clone();
            
            var dialog = _dialogs.For<FoodUnitDialogViewModel>(FoodDialogDialogsIdentifier);
            dialog.Data.FoodUnit = FoodUnitViewModel.FromModel(unitClone);
            dialog.Data.DialogTitle = "Modified food unit";
            dialog.Data.SubmitButtonTitle = "Save";

            var dialogResult = await dialog.Show();

            if (dialogResult != DialogResult.Ok)
            {
                return;
            }

            unitClone = dialog.Data.FoodUnit.ToModel();
            Food.Units.Replace(unit, unitClone);
        }

        #endregion
        
        public FoodDialogViewModel(DialogFactory dialogs)
        {
            _dialogs = dialogs;
            
            OpenAddUnitDialogCommand = new Command(OpenAddUnitDialogAsync);
            
            OpenEditUnitDialogCommand = new Command<FoodUnit>(OpenEditUnitDialogAsync);
            
            RemoveUnitCommand = new Command<FoodUnit>(RemoveUnit);
        }
    }
}
