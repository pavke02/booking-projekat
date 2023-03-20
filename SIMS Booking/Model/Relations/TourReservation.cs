using SIMS_Booking.Observer;
using SIMS_Booking.Serializer;
using System;

namespace SIMS_Booking.Model.Relations
{
    public class TourReservation : ISerializable
    {
     
        public int UserId { get; set; }
        public int TourId { get; set; }
        public int ReservationId { get; set; }

        public TourReservation() { }

        public TourReservation(int userId, int tourId) { 
        
        TourId = tourId;
        UserId = userId;
        
        }

     
        public void FromCSV(string[] values)
        {
            UserId = int.Parse(values[0]);
            TourId= int.Parse(values[1]);
            ReservationId= int.Parse(values[2]);
        }

        public string[] ToCSV()
        {
              string[] csvValues = {UserId.ToString(),TourId.ToString(), ReservationId.ToString()};
              return csvValues;
            throw new NotImplementedException();
        }




    }
}
