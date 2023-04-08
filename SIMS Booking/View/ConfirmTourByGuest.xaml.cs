using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using SIMS_Booking.Model;
using SIMS_Booking.Observer;
using SIMS_Booking.Repository;

namespace SIMS_Booking.View
{
    public partial class ConfirmTourByGuest : Window, IObserver, INotifyPropertyChanged
    {
        public ObservableCollection<User> GuestOnTour { get; set; }


        private ConfirmTourRepository _confirmTourRepository;
        private UserRepository _userRepository;
        private Tour _tour;

        public User SelectedUser { get; set; }


        public ConfirmTourByGuest(ConfirmTourRepository confirmTourRepository, Tour tour)
        {
            InitializeComponent();
            DataContext = this;

            _confirmTourRepository = confirmTourRepository;
            _confirmTourRepository.Subscribe(this);
            _tour = tour;

            GuestOnTour = new ObservableCollection<User>(_confirmTourRepository.GetGuestOnTour(tour));

            
        }

        public event PropertyChangedEventHandler? PropertyChanged;



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedUser != null)
            {
                ConfirmTour temp = new ConfirmTour();

                foreach (ConfirmTour confirmTour in _confirmTourRepository.GetAll())
                {
                    Trace.WriteLine(SelectedUser.getID());
                    if (confirmTour.IdTour == _tour.getID() && SelectedUser.getID() == confirmTour.UserId)
                    {
                        temp = confirmTour;
                    }
                }
                temp.IdCheckpoint = _tour.CurrentTourPoint;
                _confirmTourRepository.Update(temp);

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
            UpdateConfirmGuests(_confirmTourRepository.GetGuestOnTour(_tour));
        }
    }

}


