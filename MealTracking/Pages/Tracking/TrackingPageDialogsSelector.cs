using System.Windows;
using System.Windows.Controls;
using MealTracking.Pages.Tracking.Dialogs.EatingDayDialog;

namespace MealTracking.Pages.Tracking
{
    internal class TrackingPageDialogsSelector : DataTemplateSelector
    {
        public const string EatingDayDialog = "TrackingPage.EatingDayDialog";
        
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            switch (item) {
                case EatingDayDialogViewModel _:

                    return Application.Current.MainWindow?.FindResource(EatingDayDialog) as DataTemplate;
            }

            return base.SelectTemplate(item, container);
        }
    }
}