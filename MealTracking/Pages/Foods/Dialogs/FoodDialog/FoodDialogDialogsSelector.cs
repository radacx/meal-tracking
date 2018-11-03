using System.Windows;
using System.Windows.Controls;
using MealTracking.Pages.Foods.Dialogs.FoodUnitDialog;

namespace MealTracking.Pages.Foods.Dialogs.FoodDialog
{
    internal class FoodDialogDialogsSelector : DataTemplateSelector
    {
        public const string FoodUnitDialog = "FoodDialog.FoodUnitDialog";
        
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            switch (item) {
                case FoodUnitDialogViewModel _:

                    return Application.Current.MainWindow?.FindResource(FoodUnitDialog) as DataTemplate;
            }

            return base.SelectTemplate(item, container);
        }
    }
}