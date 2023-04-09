using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using SIMS_Booking.Model;
using SIMS_Booking.Observer;
using SIMS_Booking.Repository;
using SIMS_Booking.Service;

namespace SIMS_Booking.View
{
    public partial class ConfirmTourByGuest : Window, IObserver, INotifyPropertyChanged
    {
        public ObservableCollection<User> GuestOnTour { get; set; }


        private ConfirmTourCsvCrudRepository _confirmTourCsvCrudRepository;
        private UserService _userService;
        private Tour _tour;

        public User SelectedUser { get; set; }


        public ConfirmTourByGuest(ConfirmTourCsvCrudRepository confirmTourCsvCrudRepository, Tour tour)
        {
            InitializeComponent();
            DataContext = this;

            _confirmTourCsvCrudRepository = confirmTourCsvCrudRepository;
            _confirmTourCsvCrudRepository.Subscribe(this);
            _tour = tour;

            GuestOnTour = new ObservableCollection<User>(_confirmTourCsvCrudRepository.GetGuestOnTour(tour));

            
        }

        public event PropertyChangedEventHandler? PropertyChanged;



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedUser != null)
            {
                ConfirmTour temp = new ConfirmTour();

                foreach (ConfirmTour confirmTour in _confirmTourCsvCrudRepository.GetAll())
                {
                    Trace.WriteLine(SelectedUser.getID());
                    if (confirmTour.IdTour == _tour.getID() && SelectedUser.getID() == confirmTour.UserId)
                    {
                        temp = confirmTour;
                    }
                }
                temp.IdCheckpoint = _tour.CurrentTourPoint;
                _confirmTourCsvCrudRepository.Update(temp);

            }
        }

        private void UpdateConfirmGuests(List<User> users)
        {
            GuestOnTour.Clear();
            foreach (var user in users)
                GuestOnTour.Add(user);
        }

        public void Update()
        {
            UpdateConfirmGuests(_confirmTourCsvCrudRepository.GetGuestOnTour(_tour));
        }
    }

}


