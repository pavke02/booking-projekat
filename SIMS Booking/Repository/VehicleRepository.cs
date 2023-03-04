using SIMS_Booking.Model;
using SIMS_Booking.Serializer;
using System.Collections.Generic;

namespace SIMS_Booking.Repository
{
    public class VehicleRepository
    {
        private const string FilePath = "../../../Resources/Data/vehicles.csv";

        private readonly Serializer<Vehicle> _serializer;
        private List<Vehicle> _vehicles;

        public VehicleRepository()
        {
            _serializer = new Serializer<Vehicle>();
            _vehicles = new List<Vehicle>();
        }

        public List<Vehicle> Load()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Save(Vehicle vehicle)
        {
            _vehicles.Add(vehicle);
            _serializer.ToCSV(FilePath, _vehicles);
        }
    }
}
