using System.Windows;
using System.Windows.Controls;
using MealTracking.Pages.Meals.Dialogs.MealDialog;

namespace MealTracking.Pages.Meals
{
    internal class MealsPageDialogsSelector : DataTemplateSelector
    {
        public const string MealDialog = "MealsPage.MealDialog";
        
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            switch (item) {
                case MealDialogViewModel _:

                    return Application.Current.MainWindow?.FindResource(MealDialog) as DataTemplate;
            }

            return base.SelectTemplate(item, container);
        }
    }
}