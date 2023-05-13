using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using SIMS_Booking.Enums;
using SIMS_Booking.Model;
using SIMS_Booking.Model.Relations;
using SIMS_Booking.Repository;
using SIMS_Booking.Service;
using SIMS_Booking.Service.RelationsService;

namespace SIMS_Booking.UI.View.Driver
{
    /// <summary>
    /// Interaction logic for DriverAddVehicle.xaml
    /// </summary>
    public partial class DriverAddVehicle : UserControl
    {
        public DriverAddVehicle()
        {
            InitializeComponent();
        }
    }
}
