using SIMS_Booking.Utility;
using SIMS_Booking.Utility.Serializer;

namespace SIMS_Booking.Model.Relations
{
    public class ReservationOfVehicle : ISerializable, IDable
    {
        private int _id;
        public int UserId { get; set; }
        public int VehicleId { get; set; }
        public string Time { get; set; }
        public Address StartAddress { get; set; }
        public Address Destination { get; set; }

        public ReservationOfVehicle() { }

        public ReservationOfVehicle(int userId, int vehicleId, string time, Address address, Address destination)
        {
            UserId = userId;
            VehicleId = vehicleId;
            Time = time;
            StartAddress = address;
            Destination = destination;
        }
        public void FromCSV(string[] values)
        {
            _id = int.Parse(values[0]);
            UserId = int.Parse(values[1]);
            VehicleId = int.Parse(values[2]);
            Time = values[3];
            StartAddress = new Address(values[4], new Location(values[5], values[6]));
            Destination = new Address(values[7], new Location(values[8], values[9]));
        }

        public string[] ToCSV()
        {

            string[] csvValues = { _id.ToString(), UserId.ToString(), VehicleId.ToString(), Time, StartAddress.Street, StartAddress.Location.City, StartAddress.Location.Country, Destination.Street, Destination.Location.City, Destination.Location.Country};
            return csvValues;
        }

        public int GetId()
        {
            return _id;
        }

        public void SetId(int id)
        {
            _id = id;
        }
    }
}
