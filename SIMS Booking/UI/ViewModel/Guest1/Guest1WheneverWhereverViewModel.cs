using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SIMS_Booking.Commands.Guest1Commands;
using SIMS_Booking.Commands.NavigateCommands;
using SIMS_Booking.Model;
using SIMS_Booking.Service;
using SIMS_Booking.Service.NavigationService;
using SIMS_Booking.Service.RelationsService;
using SIMS_Booking.UI.Utility;
using SIMS_Booking.Utility.Observer;
using SIMS_Booking.Utility.Stores;

namespace SIMS_Booking.UI.ViewModel.Guest1
{

    public class Guest1WheneverWhereverViewModel : ViewModelBase, IObserver
    {

        private readonly AccommodationService _accommodationService;
        private readonly ReservationService _reservationService;
        private readonly ReservedAccommodationService _reservedAccommodationService;
        public List<Accommodation> AccommodationsFiltered { get; set; }
        public User LoggedUser { get; set; }

        private ObservableCollection<Accommodation> _accommodations;
        public ObservableCollection<Accommodation> Accommodations
        {
            get => _accommodations;
            set
            {
                if (value != _accommodations)
                {
                    _accommodations = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _maxGuests;
        public string MaxGuests
        {
            get => _maxGuests;
            set
            {
                if (value != _maxGuests)
                {
                    _maxGuests = value;
                    OnPropertyChanged();
                }
            }
        }

        private Accommodation _selectedAccommodation;
        public Accommodation SelectedAccommodation
        {
            get => _selectedAccommodation;
            set
            {
                if (value != _selectedAccommodation)
                {
                    _selectedAccommodation = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _minReservationDays;
        public string MinReservationDays
        {
            get => _minReservationDays;
            set
            {
                if (value != _minReservationDays)
                {
                    _minReservationDays = value;
                    OnPropertyChanged();

                }
            }
        }

        private string _userTb;
        public string UserTb
        {
            get => _userTb;
            set
            {
                if (value != _userTb)
                {
                    _userTb = value;
                    OnPropertyChanged();
                }
            }
        }

        private DateTime _startSelectedDate;

        public DateTime StartSelectedDate
        {
            get => _startSelectedDate;
            set
            {
                if (value != _startSelectedDate)
                {
                    _startSelectedDate = value;
                    OnPropertyChanged();
                }
            }
        }

        private DateTime _endSelectedDate;

        public DateTime EndSelectedDate
        {
            get => _endSelectedDate;
            set
            {
                if (value != _endSelectedDate)
                {
                    _endSelectedDate = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand NavigateBackCommand { get; }
        public ICommand ApplyWheneverCommand { get; }
        public ICommand NavigateToReserveCommand { get; }

        public Guest1WheneverWhereverViewModel(ModalNavigationStore modalNavigationStore,
            AccommodationService accommodationService, ReservationService reservationService,
            ReservedAccommodationService reservedAccommodationService, User loggedUser)
        {
            _accommodationService = accommodationService;
            _accommodationService.Subscribe(this);
            Accommodations = new ObservableCollection<Accommodation>(_accommodationService.SortBySuperOwner(_accommodationService.GetAll()));
            _reservationService = reservationService;
            _reservedAccommodationService = reservedAccommodationService;
            AccommodationsFiltered = new List<Accommodation>(_accommodationService.GetAll());
            LoggedUser = loggedUser;
            SetSuperGuest();

            NavigateToReserveCommand =
                new NavigateCommand(CreateReservationViewNavigationService(modalNavigationStore));

            NavigateBackCommand = new NavigateBackCommand(CreateCloseModalNavigationService(modalNavigationStore));
            ApplyWheneverCommand = new ApplyWheneverCommand(this, accommodationService, reservationService,
                reservedAccommodationService);
            Accommodations = new ObservableCollection<Accommodation>(_accommodationService.GetAll());
        }

        private INavigationService CreateCloseModalNavigationService(ModalNavigationStore modalNavigationStore)
        {
            return new CloseModalNavigationService(modalNavigationStore);
        }

        private INavigationService CreateReservationViewNavigationService(ModalNavigationStore modalNavigationStore)
        {
            return new ModalNavigationService<Guest1ReservationViewModel>(modalNavigationStore,
                () => new Guest1ReservationViewModel(modalNavigationStore, SelectedAccommodation, LoggedUser, _reservationService,
                    _reservedAccommodationService, this));
        }

        public void SetSuperGuest()
        {
            UserTb = LoggedUser.Username + ", Guest";
            if (_reservationService.IsSuperGuest(LoggedUser))
            {
                UserTb = LoggedUser.Username + ", Super Guest";
            }
        }


        public void UpdateAccommodations(List<Accommodation> accommodations)
        {
            Accommodations.Clear();
            foreach (var accommodation in accommodations)
                Accommodations.Add(accommodation);
        }

        public void Update()
        {
            UpdateAccommodations(_accommodationService.GetAll());
        }
    }
}
