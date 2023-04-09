using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using SIMS_Booking.Enums;
using SIMS_Booking.Model;
using SIMS_Booking.Observer;
using SIMS_Booking.Service;

namespace SIMS_Booking.View.Owner
{    
    public partial class PostponeReservationView : Window, IObserver
    {
        private PostponementService _postponementService;
        private ReservationService _reservationService;
        private User _user;

        public Postponement SelectedRequest { get; set; }  

        public ObservableCollection<Postponement> PostponementRequests { get; set; }    

        public PostponeReservationView(PostponementService postponementService, ReservationService reservationService, User user)
        {
            InitializeComponent();
            DataContext = this;

            _user = user;
            _reservationService = reservationService;

            _postponementService = postponementService;
            _postponementService.Subscribe(this);
            PostponementRequests = new ObservableCollection<Postponement>(_postponementService.GetByUserId(_user.getID()));            
        }

        private void DisableReservedDates(ReservationService reservationService)
        {
            foreach(var reservation in reservationService.GetByAccommodation(SelectedRequest.Reservation.Accommodation.getID()))
            {
                if(reservation != _reservationService.GetById(SelectedRequest.Reservation.getID()))
                {
                    var startDate = reservation.StartDate.Date;
                    var endDate = reservation.EndDate.Date;

                    var range = new CalendarDateRange(startDate, endDate);

                    reservationCalendar.BlackoutDates.Add(range);                    
                }                
            }
        }

        private void ShowRequestDetails(object sender, SelectionChangedEventArgs e)
        {
            details.Visibility = Visibility.Hidden;
            if(SelectedRequest != null)
            {
                DisableReservedDates(_reservationService);
                details.Visibility = Visibility.Visible;
                newStartDateTb.Text = SelectedRequest.NewStartDate.ToString("dd/MM/yyyy");
                newEndDateTb.Text = SelectedRequest.NewEndDate.ToString("dd/MM/yyyy");                
            }
        }

        //proveriti
        private void AcceptPostponmentRequest(object sender, RoutedEventArgs e)
        {
            _reservationService.PostponeReservation(SelectedRequest.ReservationId, SelectedRequest.NewStartDate, SelectedRequest.NewEndDate);
            string comment = "";
            PostponementStatus postponementStatus = PostponementStatus.Accepted;
            _postponementService.ReviewPostponementRequest(SelectedRequest.getID(), comment, postponementStatus);
        }

        //ToDo
        private void DeclinePostponmentRequest(object sender, RoutedEventArgs e)
        {  
            MessageBoxResult messageBoxResult = MessageBox.Show("Do you want to leave a comment?",
                "Decline comment", MessageBoxButton.YesNo);

            if (messageBoxResult == MessageBoxResult.Yes)
            {
                DeclinePostponementRequestView declinePostponementRequestView =
                    new DeclinePostponementRequestView(_postponementService, SelectedRequest);
                declinePostponementRequestView.ShowDialog();
                return;
            }

            string comment = "";
            PostponementStatus postponementStatus = PostponementStatus.Declined;
            _postponementService.ReviewPostponementRequest(SelectedRequest.getID(), comment, postponementStatus);
        }

        public void UpdatePostponements(List<Postponement> postponements)
        {
            PostponementRequests.Clear();
            foreach (var postponement in postponements)
                PostponementRequests.Add(postponement);
        }

        public void Update()
        {
            UpdatePostponements(_postponementService.GetByUserId(_user.getID()));
        }
    }
}
