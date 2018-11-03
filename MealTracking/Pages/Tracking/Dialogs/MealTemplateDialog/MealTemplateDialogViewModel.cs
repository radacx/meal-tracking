using System.Linq;
using System.Threading.Tasks;
using MealTracking.Contract.Models.Meals;
using MealTracking.Repository;
using MealTracking.Structures.Collections.Common;
using MealTracking.Structures.Dialogs;

namespace MealTracking.Pages.Tracking.Dialogs.MealTemplateDialog
{
    internal class MealTemplateDialogViewModel : DialogModelBase
    {
        private readonly Repository<MealTemplate> _mealTemplateRepository;
        
        private MealTemplate _mealTemplate;

        public MealTemplate MealTemplate
        {
            get => _mealTemplate;
            set => SetField(ref _mealTemplate, value);
        }

        public ObservableRangeCollection<MealTemplate> MealTemplates { get; } = new WpfObservableRangeCollection<MealTemplate>();
        
        public MealTemplateDialogViewModel(Repository<MealTemplate> mealTemplateRepository)
        {
            _mealTemplateRepository = mealTemplateRepository;

            Task.Run(() => MealTemplates.AddRange(_mealTemplateRepository.GetAll().OrderBy(meal => meal.Name)));
        }
    }
}