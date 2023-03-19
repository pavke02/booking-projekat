using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Printing;
using System.Reflection.Metadata;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SIMS_Booking.Model;
using SIMS_Booking.Observer;
using SIMS_Booking.Repository;
using SIMS_Booking.Repository.RelationsRepository;
using SIMS_Booking.Serializer;

namespace SIMS_Booking.View
{
    /// <summary>
    /// Interaction logic for Guest2MainView.xaml
    /// </summary>
    public partial class Guest2MainView : Window, IObserver
    {
        public ObservableCollection<Tour> Tours { get; set; }
        public Tour SelectedTour { get; set; }
        public Vehicle SelectedVehicle { get; set; }
        public User LoggedUser { get; set; }
        public int searchGuestNumber;

        private readonly TourRepository _tourRepository;
        private readonly VehicleRepository _vehicleRepository;


        public Guest2MainView(TourRepository tourRepository, User loggedUser)
        {
            InitializeComponent();
            DataContext = this;
            LoggedUser = loggedUser;

            _tourRepository = tourRepository;
            _tourRepository.Subscribe(this);
            Tours = new ObservableCollection<Tour>(tourRepository.GetAll());

        
        }
        private void UpdateTours(List<Tour> tours)
        {
            Tours.Clear();
            foreach (var tour in tours)
                Tours.Add(tour);
        }

        public void Update()
        {
            UpdateTours(_tourRepository.GetAll());
        }

        public int NextId()
        {
            Tours = new ObservableCollection<Tour>(_tourRepository.GetAll());

            if (Tours.Count < 1)
            {
                return 1;
            }
            
            return SelectedTour.getID()+ 1;
        }

        public ObservableCollection<Tour> filteredData
        {
            get
            {
                var result = Tours;
                if (searchGuestNumber != 0)
                {
                    result = new ObservableCollection<Tour>(result.Where(a => a.MaxGuests >= searchGuestNumber));
                }

                return result;
            }
        }

        public int SearchGuestNumber
        {
            get { return searchGuestNumber; }
            set
            {
                searchGuestNumber = value;
                OnPropertyChanged(nameof(filteredData));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Reserve_Taxi(object sender, RoutedEventArgs e)
        {/*
            Guest2DrivingReservationView reservationView =
                new Guest2DrivingReservationView();
            
            reservationView.Show();
           */ 
        }

        private void Reserve_Tour(object sender, RoutedEventArgs e)
        {

            if (SelectedTour == null)
            {
               
                    
                MessageBox.Show("Please select the tour!");


            }
            else
            {
                MessageBox.Show("Tour has been selected!");
            }
            
        }



    }
}
