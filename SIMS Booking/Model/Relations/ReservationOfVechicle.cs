using SIMS_Booking.Observer;
using SIMS_Booking.Serializer;

namespace SIMS_Booking.Model.Relations
{
    public class ReservationOfVehicle : ISerializable
    {
        public int UserId { get; set; }
        public int VehicleId { get; set; }
        public int ReservationId { get; set; }

        public ReservationOfVehicle() { }

        public ReservationOfVehicle(int userId, int vehicleId, int reservationId)
        {
            UserId = userId;
            VehicleId = vehicleId;
            ReservationId = reservationId;
        }

        public void FromCSV(string[] values)
        {
            UserId = int.Parse(values[0]);
            VehicleId = int.Parse(values[1]);
            ReservationId = int.Parse(values[2]);
        }

        public string[] ToCSV()
        {

            string[] csvValues = { UserId.ToString(), VehicleId.ToString(), ReservationId.ToString() };
            return csvValues;
        }
    }
}
