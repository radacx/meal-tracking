using System.Threading.Tasks;
using System.Windows.Input;
using MealTracking.Contract.Models.Meals;
using MealTracking.Pages.Meals.Dialogs.MealDialog;
using MealTracking.Structures.ViewModels;
using MealTracking.Repository;
using MealTracking.Structures;
using MealTracking.Structures.Collections.Common;
using MealTracking.Structures.Dialogs;

namespace MealTracking.Pages.Meals
{
    internal class MealsPageViewModel : ViewModelBase
    {
        public static string DialogsIdentifier => "MealsPageDialogsIdentifier";

        private readonly Repository<MealTemplate> _mealRepository;
        private readonly DialogFactory _dialogs;
        
        public ObservableRangeCollection<MealTemplate> Meals { get; } = new WpfObservableRangeCollection<MealTemplate>();


        #region MealDialog

        private async void OpenAddMealDialogAsync()
        {
            var meal = new MealTemplate();
            var dialog = _dialogs.For<MealDialogViewModel>(DialogsIdentifier);
            dialog.Data.DialogTitle = "New meal";
            dialog.Data.SubmitButtonTitle = "Create";
            dialog.Data.Meal = MealViewModel.FromModel(meal);
            
            var dialogResult = await dialog.Show();

            if (dialogResult != DialogResult.Ok)
            {
                return;
            }

            meal = dialog.Data.Meal.ToModel();
            Meals.Add(meal);
            _mealRepository.Create(meal);
        }
        
        private async void OpenEditMealDialogAsync(MealTemplate meal)
        {
            var mealClone = meal.Clone();
            var dialog = _dialogs.For<MealDialogViewModel>(DialogsIdentifier);
            dialog.Data.DialogTitle = "Modified meal";
            dialog.Data.SubmitButtonTitle = "Save";
            dialog.Data.Meal = MealViewModel.FromModel(mealClone);
                
            var dialogResult = await dialog.Show();

            if (dialogResult != DialogResult.Ok)
            {
                return;
            }
            
            mealClone = dialog.Data.Meal.ToModel();
            Meals.Replace(meal, mealClone);
            _mealRepository.Update(mealClone);
        }

        #endregion
        
        #region Commands

        public ICommand OpenAddMealDialogCommand { get; }
        
        public ICommand OpenEditMealDialogCommand { get; }

        public ICommand RemoveMealCommand { get; }
        
        #endregion


        #region Repository calls

        private void LoadDataAsync()
        {
            void Action()
            {
                Meals.AddRange(_mealRepository.GetAll());
            }

            Task.Run(Action);
        }

        private void RemoveMeal(MealTemplate meal)
        {
            Meals.Remove(meal);
            
            Task.Run(() => _mealRepository.Delete(meal));
        }

        #endregion
        
        public MealsPageViewModel(Repository<MealTemplate> mealRepository, DialogFactory dialogs)
        {
            _mealRepository = mealRepository;
            _dialogs = dialogs;

            OpenAddMealDialogCommand = new Command(OpenAddMealDialogAsync);
            
            OpenEditMealDialogCommand = new Command<MealTemplate>(OpenEditMealDialogAsync);
            
            RemoveMealCommand = new Command<MealTemplate>(RemoveMeal);
            
            LoadDataAsync();
        }
    }
}