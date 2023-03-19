using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using SIMS_Booking.Model;
using SIMS_Booking.Model.Relations;
using SIMS_Booking.Observer;
using SIMS_Booking.Repository;
using SIMS_Booking.Repository.RelationsRepository;

namespace SIMS_Booking.View
{
    /// <summary>
    /// Interaction logic for Guest2ReservationView.xaml
    /// </summary>
    public partial class Guest2DrivingReservationView : Window
    {
        public readonly Vehicle _selectedVehicle;
        private readonly VehicleRepository _vehicleRepository;
        public List<Location> Locations {get; set;}
        public List<Reservation> DrivingReservations { get; set;}
        public User LoggedUser { get; set;}
        public Address StartingAddress { get; set; }
        public Address EndingAddress { get; set;}
        public ReservationOfVehicle ReservationOfVehicle { get; set; }

        public Guest2DrivingReservationView(Vehicle selectedVehicle, User loggedUser, VehicleRepository vehicleRepository, ReservationOfVehicle reservationOfVehicle)
        {
            InitializeComponent();

            _selectedVehicle = selectedVehicle;
            _vehicleRepository = vehicleRepository;
            reservationOfVehicle = ReservationOfVehicle;
            LoggedUser= loggedUser;

            Address startingAddress = StartingAddress;
            Address endingAddress = EndingAddress;
        
        }
        
        private void Reserve(object sender, RoutedEventArgs e)
        {

        /*
            ReservationOfVehicle reservedVehicle =
                 new ReservationOfVehicle(LoggedUser.getID(), _selectedVehicle.getID(), reservation.getID());
             _reservedAccommodationRepository.Save(reservedAccommodation);
             Close();
         */
        }





    }
}
