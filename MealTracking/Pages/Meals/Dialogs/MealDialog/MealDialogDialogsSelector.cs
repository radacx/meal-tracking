using System.Windows;
using System.Windows.Controls;
using MealTracking.Pages.Meals.Dialogs.MealFoodDialog;

namespace MealTracking.Pages.Meals.Dialogs.MealDialog
{
    public class MealDialogDialogsSelector : DataTemplateSelector
    {
        public const string MealFoodDialog = "MealDialog.MealFoodDialog";
        
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            switch (item) {
                case MealFoodDialogViewModel _:

                    return Application.Current.MainWindow?.FindResource(MealFoodDialog) as DataTemplate;
            }

            return base.SelectTemplate(item, container);
        }
    }
}