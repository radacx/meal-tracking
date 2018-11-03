using System.Threading.Tasks;
using System.Windows.Input;
using MealTracking.Constants;
using MealTracking.Contract.Models.Foods;
using MealTracking.Pages.Foods.Dialogs.FoodDialog;
using MealTracking.Service.Services;
using MealTracking.Structures;
using MealTracking.Structures.Collections.Common;
using MealTracking.Structures.Dialogs;
using MealTracking.Structures.ViewModels;

namespace MealTracking.Pages.Foods
{
    internal class FoodsPageViewModel : ViewModelBase
    {
        private readonly FoodService _foodService;
        private readonly DialogFactory _dialogs;
        
        public ObservableRangeCollection<Food> Foods { get; } = new WpfObservableRangeCollection<Food>();

        public static string DialogsIdentifier => "FoodsPageDialogsIdentifier";
        
        #region Commands

        public ICommand OpenAddFoodDialogCommand { get; }
        
        public ICommand OpenEditFoodDialogCommand { get; }
        
        public ICommand RemoveFoodCommand { get; }

        #endregion


        #region Repository calls

        private void LoadDataAsync() => Task.Run(() => Foods.AddRange(_foodService.GetAll()));

        private void RemoveFoodAsync(Food food) => Task.Run(() => _foodService.Delete(food));
        
        #endregion


        #region Food dialog

        private async void OpenAddFoodDialogAsync()
        {
            var food = new Food();
            food.DefaultFoodUnit.FoodUnit.Grams = FoodUnitConstants.DefaultUnitGrams;
            food.DefaultFoodUnit.FoodUnit.Name = FoodUnitConstants.DefaultUnitName;
            
            var dialog = _dialogs.For<FoodDialogViewModel>(DialogsIdentifier);
            dialog.Data.Food = FoodViewModel.FromModel(food);
            dialog.Data.DialogTitle = "New food";
            dialog.Data.SubmitButtonTitle = "Create";
            
            var dialogResult = await dialog.Show();

            if (dialogResult != DialogResult.Ok)
            {
                return;
            }

            food = dialog.Data.Food.ToModel();
            Foods.Add(food);
            _foodService.Create(food);
        }

        private async void OpenEditFoodDialogAsync(Food food)
        {
            var foodClone = food.Clone();
 
            var dialog = _dialogs.For<FoodDialogViewModel>(DialogsIdentifier);
            dialog.Data.Food = FoodViewModel.FromModel(foodClone);
            dialog.Data.DialogTitle = "Modified food";
            dialog.Data.SubmitButtonTitle = "Save"; 
                    
            var dialogResult = await dialog.Show();

            if (dialogResult != DialogResult.Ok)
            {
                return;
            }

            foodClone = dialog.Data.Food.ToModel();
            Foods.Replace(food, foodClone);
            
            _foodService.Update(foodClone, food);
        }

        #endregion

        private void RemoveFood(Food food)
        {
            Foods.Remove(food);

            RemoveFoodAsync(food);
        }
        
        public FoodsPageViewModel(FoodService foodService, DialogFactory dialogs)
        {
            _foodService = foodService;
            _dialogs = dialogs;

            OpenAddFoodDialogCommand = new Command(OpenAddFoodDialogAsync);
            
            OpenEditFoodDialogCommand = new Command<Food>(OpenEditFoodDialogAsync);
            
            RemoveFoodCommand = new Command<Food>(RemoveFood);
            
            LoadDataAsync();
        }
    }
}
