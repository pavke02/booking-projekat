using SIMS_Booking.Observer;
using SIMS_Booking.Serializer;
using System;

namespace SIMS_Booking.Model.Relations
{
    public class ReservationOfVehicle : ISerializable
    {
        public int UserId { get; set; }
        public int VehicleId { get; set; }
        public DateTime Date { get; set; }
        public string Address { get; set; }

        public ReservationOfVehicle() { }

        public ReservationOfVehicle(int userId, int vehicleId, DateTime date, string address)
        {
            UserId = userId;
            VehicleId = vehicleId;
            Date = date;
            Address = address;
        }
        public void FromCSV(string[] values)
        {
            UserId = int.Parse(values[0]);
            VehicleId = int.Parse(values[1]);
            Date = DateTime.Parse(values[2]);
            Address = values[3];
        }

        public string[] ToCSV()
        {

            string[] csvValues = { UserId.ToString(), VehicleId.ToString(), Date.ToString(), Address };
            return csvValues;
        }
    }
}
