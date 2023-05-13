using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using SIMS_Booking.Model;
using SIMS_Booking.Service;
using SIMS_Booking.Utility.Observer;

namespace SIMS_Booking.UI.View
{

    public partial class ConfirmByGuest : Window, IObserver, INotifyPropertyChanged
    {
        public ObservableCollection<User> GuestOnTour { get; set; }
        public ObservableCollection<int> NumberOfGuestsOnTour { get; set; }
        private UserService _userService;
        private ConfirmTourService _confirmTourService;
        private Tour _tour;
        public event PropertyChangedEventHandler? PropertyChanged;
        public User SelectedUser { get; set; }


        public ConfirmByGuest(ConfirmTourService confirmTourService, Tour tour)
        {
            InitializeComponent();
            DataContext = this;
            _confirmTourService = confirmTourService;
            _confirmTourService.Subscribe(this);
            _tour = tour;
            GuestOnTour = new ObservableCollection<User>(_confirmTourService.GetGuestOnTour(tour));
        }


        private void UpdateConfirmGuests(List<User> users)
        {
            GuestOnTour.Clear();
            foreach (var user in users)
                GuestOnTour.Add(user);
        }

        public void Update()
        {
            UpdateConfirmGuests(_confirmTourService.GetGuestOnTour(_tour));
        }

        private void ComeOnTour(object sender, RoutedEventArgs e)
        {
            if (SelectedUser != null)
            {
                ConfirmTour temp = new ConfirmTour();
                foreach (ConfirmTour confirmTour in _confirmTourService.GetAll())
                {
                    Trace.WriteLine(SelectedUser.GetId());
                    if (confirmTour.IdTour == _tour.GetId() && SelectedUser.GetId() == confirmTour.UserId)
                        temp = confirmTour;

                }

                temp.IdCheckpoint = _tour.CurrentTourPoint;
                _confirmTourService.Update(temp);
            }
        }
    }
}
