using MealTracking.Structures.ViewModels;

namespace MealTracking.Structures.Dialogs
{
    internal abstract class DialogModelBase : ViewModelBase
    {
        public string SubmitButtonTitle { get; set; }
        
        public string DialogTitle { get; set; }
    }
}