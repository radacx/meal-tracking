using System.Windows;
using System.Windows.Controls;
using MealTracking.Pages.Foods.Dialogs.FoodDialog;

namespace MealTracking.Pages.Foods
{
    internal class FoodsPageDialogsSelector : DataTemplateSelector {
        public const string FoodDialog = "FoodsPage.FoodDialog";
        
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            switch (item) {
                case FoodDialogViewModel _:

                    return Application.Current.MainWindow?.FindResource(FoodDialog) as DataTemplate;
            }

            return base.SelectTemplate(item, container);
        }
    }
}