using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using MealTracking.Contract.Models.Foods;
using MealTracking.Repository;
using MealTracking.Structures.Collections.Common;
using MealTracking.Structures.Dialogs;

namespace MealTracking.Pages.Meals.Dialogs.MealFoodDialog
{
    internal class MealFoodDialogViewModel : DialogModelBase
    {
        private readonly Repository<Food> _foodRepository;
        
        private MealFoodViewModel _mealFood;

        public MealFoodViewModel MealFood
        {
            get => _mealFood;
            set => SetField(ref _mealFood, value);
        }

        public ObservableRangeCollection<Food> Foods { get; } = new WpfObservableRangeCollection<Food>(); 

        public ObservableRangeCollection<FoodUnit> FoodUnits { get; } = new WpfObservableRangeCollection<FoodUnit>();
        
        public MealFoodDialogViewModel(Repository<Food> foodRepository)
        {
            _foodRepository = foodRepository;

            Task.Run(() => Foods.AddRange(_foodRepository.GetAll().OrderBy(food => food.Name)));

            PropertyChanged += OnPropertyChanged;  
        }

        private void SetFoodUnits()
        {
            if (MealFood.Food != null)
            {
                FoodUnits.ReplaceRange(MealFood.Food.Units.Concat(new[] { MealFood.Food.DefaultFoodUnit.FoodUnit }.OrderBy(foodUnit => foodUnit.Name)));
            }
        }
        
        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName) {
                case nameof(MealFood):
                    MealFood.PropertyChanged += OnPropertyChanged;
                    SetFoodUnits();
                    
                    break;
                case nameof(MealFood.Food):
                    SetFoodUnits();

                    break;
            }
        }
    }
}