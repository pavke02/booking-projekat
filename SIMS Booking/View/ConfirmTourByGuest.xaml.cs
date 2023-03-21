using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using SIMS_Booking.Model;
using SIMS_Booking.Observer;
using SIMS_Booking.Repository;
using SIMS_Booking.Serializer;
using SIMS_Booking.State;

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
                    Trace.WriteLine(SelectedUser.ID);
                    if (confirmTour.IdTour == _tour.ID && SelectedUser.getID() == confirmTour.UserId)
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


