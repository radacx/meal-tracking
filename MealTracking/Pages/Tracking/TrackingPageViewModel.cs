using System;
using System.Threading.Tasks;
using System.Windows.Input;
using MealTracking.Contract.Models.Days;
using MealTracking.Pages.Tracking.Dialogs.EatingDayDialog;
using MealTracking.Repository;
using MealTracking.Structures;
using MealTracking.Structures.Collections.Common;
using MealTracking.Structures.Dialogs;
using MealTracking.Structures.ViewModels;

namespace MealTracking.Pages.Tracking
{
    internal class TrackingPageViewModel : ViewModelBase
    {
        public static string DialogsIdentifier => "TrackingPageDialogsIdentifier";

        private readonly Repository<EatingDay> _eatingDayRepository;

        private readonly DialogFactory _dialogs;
        
        public ObservableRangeCollection<EatingDay> Days { get; } = new WpfObservableRangeCollection<EatingDay>();
        
        #region Commands

        public ICommand OpenAddEatingDayDialogCommand { get; }
        
        public ICommand OpenEditEatingDayDialogCommand { get; }
        
        public ICommand RemoveEatingDayCommand { get; }

        #endregion


        #region MealDayDialog

        private async void OpenAddEatingDayDialogAsync()
        {
            var day = new EatingDay
            {
                Date = DateTime.Today
            };
            
            var dialog = _dialogs.For<EatingDayDialogViewModel>(DialogsIdentifier);
            dialog.Data.DialogTitle = "New day";
            dialog.Data.SubmitButtonTitle = "Create";
            dialog.Data.EatingDay = EatingDayViewModel.FromModel(day);
                
            var dialogResult = await dialog.Show();

            if (dialogResult != DialogResult.Ok)
            {
                return;
            }

            day = dialog.Data.EatingDay.ToModel();
            Days.Add(day);
            _eatingDayRepository.Create(day);
        }

        private async void OpenEditEatingDayDialogAsync(EatingDay day)
        {
            var dayClone = day.Clone();
            
            var dialog = _dialogs.For<EatingDayDialogViewModel>(DialogsIdentifier);
            dialog.Data.DialogTitle = "Modified day";
            dialog.Data.SubmitButtonTitle = "Save";
            dialog.Data.EatingDay = EatingDayViewModel.FromModel(dayClone);
                
            var dialogResult = await dialog.Show();

            if (dialogResult != DialogResult.Ok)
            {
                return;
            }

            dayClone = dialog.Data.EatingDay.ToModel();
            Days.Replace(day, dayClone);
            _eatingDayRepository.Update(dayClone);
        }

        #endregion


        #region Repository calls

        private void LoadData()
        {
            Days.AddRange(_eatingDayRepository.GetAll());
        }
        
        private void RemoveDay(EatingDay day)
        {
            Days.Remove(day);
            
            Task.Run(() => _eatingDayRepository.Delete(day));
        }

        #endregion
        
        public TrackingPageViewModel(Repository<EatingDay> eatingDayRepository, DialogFactory dialogs)
        {
            _eatingDayRepository = eatingDayRepository;
            _dialogs = dialogs;
            OpenAddEatingDayDialogCommand = new Command(OpenAddEatingDayDialogAsync);
            
            OpenEditEatingDayDialogCommand = new Command<EatingDay>(OpenEditEatingDayDialogAsync);
            
            RemoveEatingDayCommand = new Command<EatingDay>(RemoveDay);

            Task.Run(LoadData);
        }
    }
}