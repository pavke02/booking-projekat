using SIMS_Booking.Utility;
using SIMS_Booking.Utility.Serializer;

namespace SIMS_Booking.Model.Relations
{
    public class ReservationOfVehicle : ISerializable, IDable
    {
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
            UserId = int.Parse(values[0]);
            VehicleId = int.Parse(values[1]);
            Time = values[2];
            StartAddress = new Address(values[3], new Location(values[4], values[5]));
            Destination = new Address(values[6], new Location(values[7], values[8]));
        }

        public string[] ToCSV()
        {

            string[] csvValues = { UserId.ToString(), VehicleId.ToString(), Time, StartAddress.Street, StartAddress.Location.City, StartAddress.Location.Country, Destination.Street, Destination.Location.City, Destination.Location.Country};
            return csvValues;
        }

        public int GetId()
        {
            return UserId;
        }

        public void SetId(int id)
        {
            UserId = id;
        }
    }
}
