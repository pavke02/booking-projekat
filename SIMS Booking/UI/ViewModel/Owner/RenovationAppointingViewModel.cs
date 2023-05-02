using Microsoft.TeamFoundation.WorkItemTracking.Process.WebApi.Models;
using SIMS_Booking.Commands.NavigateCommands;
using SIMS_Booking.Commands.OwnerCommands;
using SIMS_Booking.Model;
using SIMS_Booking.Service;
using SIMS_Booking.Service.NavigationService;
using SIMS_Booking.UI.Utility;
using SIMS_Booking.Utility.Stores;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Input;

namespace SIMS_Booking.UI.ViewModel.Owner
{
    public class RenovationAppointingViewModel : ViewModelBase, IDataErrorInfo
    {
        private readonly ReservationService _reservationService;
        private readonly RenovationAppointmentService _renovationAppointmentService;

        private readonly ModalNavigationStore _modalNavigationStore;
        private readonly Accommodation _accommodation;

        #region Property
        public Dictionary<string, string> ErrorCollection { get; private set; } = new Dictionary<string, string>();
        public string Error { get { return null; } }

        private string _startDate;
        public string StartDate
        {
            get => _startDate;
            set
            {
                if (value != _startDate)
                {
                    _startDate = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _endDate;
        public string EndDate
        {
            get => _endDate;
            set
            {
                if (value != _endDate)
                {
                    _endDate = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _description;

        public string Description
        {
            get => _description;
            set
            {
                if (value != _description)
                {
                    _description = value;
                    OnPropertyChanged();
                }
            }
        } 
        #endregion

        public ICommand NavigateBackCommand { get; }
        public ICommand AppointRenovatingCommand { get; }

        public RenovationAppointingViewModel(Accommodation selectedAccommodation, ReservationService reservationService, RenovationAppointmentService renovationAppointmentService, ModalNavigationStore modalNavigationStore)
        {
            _accommodation = selectedAccommodation;
            _modalNavigationStore = modalNavigationStore;
            _reservationService = reservationService;
            _renovationAppointmentService = renovationAppointmentService;

            AppointRenovatingCommand = new AppointRenovatingCommand(this, _renovationAppointmentService);
            NavigateBackCommand =
                new NavigateBackCommand(CreateCloseModalNavigationService());
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

        public delegate void BlackoutDatesChangedHandler(List<CalendarDateRange> reservedDates);
        public event BlackoutDatesChangedHandler? BlackoutDatesChangedEvent;

        public void DisableReservedDates()
        {
            List<CalendarDateRange> blackoutDates = new List<CalendarDateRange>();
            foreach (var reservation in _reservationService.GetByAccommodation(_accommodation.getID()))
            {
                var startDate = reservation.StartDate.Date;
                var endDate = reservation.EndDate.Date;

                var range = new CalendarDateRange(startDate, endDate);

                blackoutDates.Add(range);
            }

            //kada se napuni lista sa datumima koji su onemoguceni, obavesti se PostponeReservationView i ti datumi se oznace na kalendaru
            BlackoutDatesChangedEvent?.Invoke(blackoutDates);
        }

        private INavigationService CreateCloseModalNavigationService()
        {
            return new CloseModalNavigationService(_modalNavigationStore);
        }

        #region Validation

        public string this[string name]
        {
            get
            {
                string result = null;
                switch (name)
                {
                    case "StartDate":
                        if (string.IsNullOrWhiteSpace(StartDate))
                            result = "You must select start date!";
                        break;
                    case "EndDate":
                        if (string.IsNullOrEmpty(EndDate))
                            result = "You must select end date!";
                        break;
                    case "Description":
                        if (string.IsNullOrEmpty(Description))
                            result = "Description can not be empty";
                        break;
                }

                if (ErrorCollection.ContainsKey(name))
                    ErrorCollection[name] = result;
                else if (result != null)
                    ErrorCollection.Add(name, result);

                OnPropertyChanged("ErrorCollection");
                return result;
            }
        }
        #endregion
    }
}
