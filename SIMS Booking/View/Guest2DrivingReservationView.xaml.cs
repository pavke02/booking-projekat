using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SIMS_Booking.Model;
using SIMS_Booking.Model.Relations;
using SIMS_Booking.Observer;
using SIMS_Booking.Repository;
using SIMS_Booking.Repository.RelationsRepository;

namespace SIMS_Booking.View;

/// <summary>
/// Interaction logic for Guest2ReservationView.xaml
/// </summary>
public partial class Guest2DrivingReservationView : Window
{
    public readonly Vehicle _selectedVehicle;
    private readonly VehicleCsvCrudRepository _vehicleCsvCrudRepository;
    public List<Location> Locations { get; set; }
    public List<DriverLocations> DrivingReservations { get; set; }
    public User LoggedUser { get; set; }
    public Address StartingAddress { get; set; }
    public Address EndingAddress { get; set; }
    public ReservationOfVehicle ReservationOfVehicle { get; set; }
    public VehicleReservationCsvCrudRepository VehicleReservationCsvCrudRespository;
    private string searchLocation;
    public ObservableCollection<DriverLocations> drivers { get; set; }
    public DriverLocations selectedDriver { get; set; }


    private DriverLocationsCsvCrudRepository _driverLocationsCsvCrudRespository;


    public Guest2DrivingReservationView(Vehicle selectedVehicle, User loggedUser)
    {
        InitializeComponent();

        DataContext = this;
        _selectedVehicle = selectedVehicle;
        selectedDriver = new DriverLocations();
        _vehicleCsvCrudRepository = new VehicleCsvCrudRepository();
        _driverLocationsCsvCrudRespository = new DriverLocationsCsvCrudRepository();
        LoggedUser = loggedUser;
        drivers = new ObservableCollection<DriverLocations>(_driverLocationsCsvCrudRespository.GetAll());

        var startingAddress = StartingAddress;
        var endingAddress = EndingAddress;
    }

    private void Reserve(object sender, RoutedEventArgs e)
    {
        var reservedVehicle =
            new ReservationOfVehicle(LoggedUser.getID(), _vehicleCsvCrudRepository.GetVehicleByUserID(selectedDriver.DriverId).getID(), DateTime.Parse(TimeofDepartureTextBox.Text), StartingAddressTextBox.Text);
        VehicleReservationCsvCrudRespository.Save(reservedVehicle);
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
            var result = _driverLocationsCsvCrudRespository.GetAll();

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
        var result = _driverLocationsCsvCrudRespository.GetAll();


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
