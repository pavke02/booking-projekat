using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using SIMS_Booking.Commands.NavigateCommands;
using SIMS_Booking.Model;
using SIMS_Booking.Service;
using SIMS_Booking.Service.NavigationService;
using SIMS_Booking.UI.Utility;
using SIMS_Booking.Utility.Stores;

namespace SIMS_Booking.UI.ViewModel.Guide
{
    public partial class StartingTourViewModel : ViewModelBase
    {
        public ICommand BackCommand { get; }
        public ICommand ListOfGuests { get; }
        private Tour _tour;
        private ConfirmTourService _confirmTourService;
        private bool _checked;
        private CreateTourViewModel _viewModel;
        private ModalNavigationStore _modalNavigationStore;
        private NavigationStore _navigationStore;
        public bool Checked
        {
            get => _checked;
            set
            {
                if (value != _checked)
                {
                    _checked = value;
                    OnPropertyChanged();
                }
            }


        }
        public ObservableCollection<TourPoint> Checkpoints { get; set; }


        public StartingTourViewModel(Tour tour, ConfirmTourService confirmTourService, CreateTourViewModel viewModel, ModalNavigationStore modalNavigationStore,NavigationStore navigationStore)
        {
            _tour = tour;
            _confirmTourService = confirmTourService;
            Checkpoints = new ObservableCollection<TourPoint>(_tour.TourPoints);
            _viewModel = viewModel;
            _modalNavigationStore = modalNavigationStore;

            BackCommand = new NavigateCommand(CreateService(navigationStore));
        }

        private INavigationService CreateConfirmTourByGuestNavigationService(NavigationStore navigationStore)
        {
            return new NavigationService<ConfirmByGuestViewModel>
                 (navigationStore, () => new ConfirmByGuestViewModel(_confirmTourService, _tour));
        }

        private INavigationService CreateService(NavigationStore navigationStore)
        {
            return new NavigationService<CreateTourViewModel>
                 (navigationStore, () => _viewModel);

        }

        [RelayCommand]
        private void NextCheckPoint()
        {
            for (int i = 0; i < Checkpoints.Count; i++)
            {
                if ((Checkpoints[i].CheckedCheckBox) && (i != Checkpoints.Count - 1))
                {
                    (new NavigateCommand(CreateConfirmTourByGuestNavigationService(_navigationStore))).Execute(null);

                    Checkpoints[i].CheckedCheckBox = false;
                    Checkpoints[i + 1].CheckedCheckBox = true;
                    _tour.CurrentTourPoint = i + 1;
                    OnPropertyChanged();

                    break;
                }

                if (i == Checkpoints.Count - 1)
                {
                    //Window.GetWindow(this).Close();
                    Checkpoints[0].CheckedCheckBox = true;
                    _tour.CurrentTourPoint = 0;
                    for (int j = 1; j < Checkpoints.Count; j++)
                    {
                        Checkpoints[j].CheckedCheckBox = false;
                    }
                }
            }
        }
        [RelayCommand]
        private void EmergencyStopTour()
        {
            Checkpoints[0].CheckedCheckBox = true;
            _tour.CurrentTourPoint = 0;
            for (int j = 1; j < Checkpoints.Count; j++)
            {
                Checkpoints[j].CheckedCheckBox = false;
            }
            foreach (ConfirmTour confirmTour in _confirmTourService.GetAll().ToList())
            {
                if (confirmTour.IdTour == _tour.GetId())
                {
                    _confirmTourService.Delete(confirmTour);
                }
            }
            //Window.GetWindow(this).Close();
        }

    }
}
