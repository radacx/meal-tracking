using System.Windows;
using System.Windows.Controls;
using MealTracking.Pages.Meals.Dialogs.MealFoodDialog;
using MealTracking.Pages.Tracking.Dialogs.MealTemplateDialog;

namespace MealTracking.Pages.Tracking.Dialogs.DayMealDialog
{
    internal class DayMealDialogDialogsSelector : DataTemplateSelector
    {
        public const string MealFoodDialog = "MealDialog.DayMealFoodDialog";
        public const string MealTemplateDialog = "EatingDayDialog.MealTemplateDialog";
        
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            switch (item) {
                case MealFoodDialogViewModel _:

                    return Application.Current.MainWindow?.FindResource(MealFoodDialog) as DataTemplate;
                case MealTemplateDialogViewModel _:
                    
                    return Application.Current.MainWindow?.FindResource(MealTemplateDialog) as DataTemplate;
            }

            return base.SelectTemplate(item, container);
        }
    }
}