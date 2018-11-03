using MealTracking.Pages.Foods;
using MealTracking.Pages.Meals;
using MealTracking.Pages.Tracking;
using MealTracking.Structures.ViewModels;

namespace MealTracking.Pages
{
    internal class MainWindowViewModel : ViewModelBase
    {
        public FoodsPageViewModel FoodsPage { get; }
        
        public MealsPageViewModel MealsPage { get; }
        
        public TrackingPageViewModel TrackingPage { get; }
        
        public MainWindowViewModel(FoodsPageViewModel foodsPage, MealsPageViewModel mealsPage, TrackingPageViewModel trackingPage)
        {
            FoodsPage = foodsPage;
            MealsPage = mealsPage;
            TrackingPage = trackingPage;
        }
    }
}