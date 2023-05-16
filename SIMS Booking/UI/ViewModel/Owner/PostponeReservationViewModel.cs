using SIMS_Booking.Commands.NavigateCommands;
using SIMS_Booking.Commands.OwnerCommands;
using SIMS_Booking.Model;
using SIMS_Booking.Service;
using SIMS_Booking.Service.NavigationService;
using SIMS_Booking.UI.Utility;
using SIMS_Booking.Utility.Observer;
using SIMS_Booking.Utility.Stores;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;

namespace SIMS_Booking.UI.ViewModel.Owner
{
    public class PostponeReservationViewModel : ViewModelBase, IObserver
    {
        private readonly PostponementService _postponementService;
        private readonly ReservationService _reservationService;
        private readonly User _user;

        public ICommand AcceptPostponementRequestCommand { get; }
        public ICommand NavigateToDeclinePostponementRequestCommand { get; }
        public ICommand NavigateBackCommand { get; }

        #region Property
        public ObservableCollection<Postponement> PostponementRequests { get; set; }

        private Postponement _selectedRequest;
        public Postponement SelectedRequest
        {
            get => _selectedRequest;
            set
            {
                if (value != _selectedRequest)
                {
                    _selectedRequest = value;
                    OnPropertyChanged();
                    ShowRequestDetails();
                }
            }
        }

        private string _newStartDate;
        public string NewStartDate
        {
            get => _newStartDate;
            set
            {
                if (value != _newStartDate)
                {
                    _newStartDate = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _newEndDate;
        public string NewEndDate
        {
            get => _newEndDate;
            set
            {
                if (value != _newEndDate)
                {
                    _newEndDate = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isVisible;
        public bool IsVisible
        {
            get => _isVisible;
            set
            {
                if (value != _isVisible)
                {
                    _isVisible = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        public PostponeReservationViewModel(ModalNavigationStore modalNavigationStore, PostponementService postponementService,
            ReservationService reservationService, User user)
        {
            _user = user;
            _reservationService = reservationService;

            _postponementService = postponementService;
            _postponementService.Subscribe(this);
            PostponementRequests = new ObservableCollection<Postponement>(_postponementService.GetByUserId(_user.GetId()));

            AcceptPostponementRequestCommand =
                new AcceptPostponementRequestCommand(this, _reservationService, _postponementService);

            NavigateBackCommand =
                new NavigateBackCommand(CreateCloseModalNavigationService(modalNavigationStore));
            NavigateToDeclinePostponementRequestCommand = 
                new NavigateCommand(CreateDeclinePostponementNavigationService(modalNavigationStore));
        }

        //Unsubscribe sve koji su subscribe na njega
        public override void Dispose()
        {
            base.Dispose();
            foreach (var date in BlackoutDatesChangedEvent.GetInvocationList())
            {
                BlackoutDatesChangedEvent -= date as BlackoutDatesChangedHandler;
            }
        }

        private void ShowRequestDetails()
        {
            IsVisible = true;
            if (SelectedRequest != null)
            {
                DisableReservedDates(_reservationService);
                NewStartDate = SelectedRequest.NewStartDate.ToString("dd/MM/yyyy");
                NewEndDate = SelectedRequest.NewEndDate.ToString("dd/MM/yyyy");
            }
        }

        //event hendler za promene datuma
        public delegate void BlackoutDatesChangedHandler(List<CalendarDateRange> reservedDates);
        public event BlackoutDatesChangedHandler? BlackoutDatesChangedEvent;
        private void DisableReservedDates(ReservationService reservationService)
        {
            List<CalendarDateRange> blackoutDates = new List<CalendarDateRange>();
            foreach (var reservation in reservationService.GetActiveByAccommodation(SelectedRequest.Reservation.Accommodation.GetId()))
            {
                if (reservation != _reservationService.GetById(SelectedRequest.Reservation.GetId()))
                {
                    var startDate = reservation.StartDate.Date;
                    var endDate = reservation.EndDate.Date;
        
                    var range = new CalendarDateRange(startDate, endDate);

                    blackoutDates.Add(range);
                }
            }

            //kada se napuni lista sa datumima koji su onemoguceni, obavesti se PostponeReservationView i ti datumi se oznace na kalendaru
            BlackoutDatesChangedEvent?.Invoke(blackoutDates);
        }

        private INavigationService CreateCloseModalNavigationService(ModalNavigationStore modalNavigationStore)
        {
            return new CloseModalNavigationService(modalNavigationStore);
        }

        private INavigationService CreateDeclinePostponementNavigationService(ModalNavigationStore modalNavigationStore)
        {
            return new ModalNavigationService<DeclinePostponementRequestViewModel>
            (modalNavigationStore, () => new DeclinePostponementRequestViewModel(modalNavigationStore, _postponementService,
                SelectedRequest));
        }

        #region Update
        public void UpdatePostponements(List<Postponement> postponements)
        {
            PostponementRequests.Clear();
            foreach (var postponement in postponements)
                PostponementRequests.Add(postponement);
        }

        public void Update()
        {
            UpdatePostponements(_postponementService.GetByUserId(_user.GetId()));
        }
        #endregion
    }
}
