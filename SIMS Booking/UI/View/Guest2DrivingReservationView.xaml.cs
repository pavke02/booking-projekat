using SIMS_Booking.Model;
using SIMS_Booking.Model.Relations;
using SIMS_Booking.Service;
using SIMS_Booking.Service.RelationsService;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace SIMS_Booking.UI.View;

/// <summary>
/// Interaction logic for Guest2ReservationView.xaml
/// </summary>
public partial class Guest2DrivingReservationView : Window
{


    public readonly Vehicle _selectedVehicle;
 
    private readonly VehicleReservationService _vehicleReservationService;
    public List<Location> Locations { get; set; }
    public List<DriverLocations> DrivingReservations { get; set; }
    public User LoggedUser { get; set; }
    public Address StartingAddress { get; set; }
    public Address EndingAddress { get; set; }
    public ReservationOfVehicle ReservationOfVehicle { get; set; }
    
    private readonly VehicleService _vehicleService;
    private string searchLocation;
    public ObservableCollection<DriverLocations> drivers { get; set; }
    public DriverLocations selectedDriver { get; set; }


    
    private DriverLocationsService _driverLocationsService;




    public Guest2DrivingReservationView(Vehicle selectedVehicle, User loggedUser, VehicleReservationService vehicleReservationService, VehicleService vehicleService, DriverLocationsService driverLocationsService)
    {
        InitializeComponent();

        DataContext = this;
        _selectedVehicle = selectedVehicle;
        selectedDriver = new DriverLocations();
      
       
        _vehicleReservationService = vehicleReservationService;
        _vehicleService = vehicleService;
        _driverLocationsService = driverLocationsService;

        LoggedUser = loggedUser;
   
        drivers = new ObservableCollection<DriverLocations>(_driverLocationsService.GetAll());


        var startingAddress = StartingAddress;
        var endingAddress = EndingAddress;
    }

    private void Reserve(object sender, RoutedEventArgs e)
    {
        var reservedVehicle =
            new ReservationOfVehicle(LoggedUser.GetId(), _vehicleService.GetVehicleByUserID(selectedDriver.DriverId).GetId(), TimeofDepartureTextBox.Text, new Address(StartingAddressTextBox.Text, selectedDriver.Location), new Address(FinalAddressTextBox.Text, selectedDriver.Location));
        _vehicleReservationService.Save(reservedVehicle);

        MessageBox.Show("Uspešno ste rezervisali vožnju!");

        Close();
    }

    public string SearchLocation
    {
        get => searchLocation;
        set
        {
            searchLocation = value;
            UpdateDriverList();
            OnPropertyChanged(nameof(filteredData));
        }
    }

    public ObservableCollection<DriverLocations> filteredData
    {
        get
        {
            var result = _driverLocationsService.GetAll();

            if (!string.IsNullOrEmpty(searchLocation))
                drivers = new ObservableCollection<DriverLocations>(result.Where(a =>
                    a.Location.City.ToLower().Contains(searchLocation) ||
                    a.Location.Country.ToLower().Contains(searchLocation)));

            return drivers;
        }
    }

    public void UpdateDriverList()
    {
        drivers.Clear();
      
        var result = _driverLocationsService.GetAll();

        if (!string.IsNullOrEmpty(searchLocation))
            result = new List<DriverLocations>(result.Where(a =>
                 a.Location.City.ToLower().Contains(searchLocation) ||
                 a.Location.Country.ToLower().Contains(searchLocation)));
        foreach (var driver in result)
        {

            drivers.Add(driver);


        }


    }


    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
