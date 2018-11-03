using System.Windows;
using System.Windows.Controls;
using MealTracking.Pages.Tracking.Dialogs.DayMealDialog;

namespace MealTracking.Pages.Tracking.Dialogs.EatingDayDialog
{
    internal class EatingDayDialogDialogsSelector : DataTemplateSelector
    {
        public const string DayMealDialog = "EatingDayDialog.DayMealDialog";
        
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            switch (item) {
                case DayMealDialogViewModel _:

                    return Application.Current.MainWindow?.FindResource(DayMealDialog) as DataTemplate;
            }

            return base.SelectTemplate(item, container);
        }
    }
}