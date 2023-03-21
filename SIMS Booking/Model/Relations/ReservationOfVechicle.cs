using SIMS_Booking.Observer;
using SIMS_Booking.Serializer;

namespace SIMS_Booking.Model.Relations
{
    public class ReservationOfVehicle : ISerializable
    {
        public int UserId { get; set; }
        public int VehicleId { get; set; }
        

        public ReservationOfVehicle() { }

        public ReservationOfVehicle(int userId, int vehicleId)
        {
            UserId = userId;
            VehicleId = vehicleId;
            
        }

        public void FromCSV(string[] values)
        {
            UserId = int.Parse(values[0]);
            VehicleId = int.Parse(values[1]);
            
        }

        public string[] ToCSV()
        {

            string[] csvValues = { UserId.ToString(), VehicleId.ToString()};
            return csvValues;
        }
    }
}
