using System;
using System.Collections.Generic;
using System.Linq;
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
    public partial class Guest2ReservationView : Window
    {
        public readonly Vehicle _selectedVehicle;
        private readonly VehicleRepository _vehicleRepository;

        public User LoggedUser { get; set;}



        public Guest2ReservationView()
        {
            InitializeComponent();
        }
    }
}
