using SIMS_Booking.Observer;
using SIMS_Booking.Serializer;
using System;

namespace SIMS_Booking.Model.Relations
{
    public class TourReservation : ISerializable
    {
     
        public int Id { get; set; }
        
        public int TourId { get; set; }
        public DateTime StartedTime { get; set; }

        public int GuideId { get; set; }
        

        public TourReservation() { }

        public TourReservation(int tourId, DateTime startedTime, int guideId) { 
        
        TourId = tourId;
        StartedTime = startedTime;
        GuideId = guideId;
        
        }

     
        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            TourId= int.Parse(values[1]);
            GuideId= int.Parse(values[2]);

        }

        public string[] ToCSV()
        {
              string[] csvValues = {Id.ToString(),TourId.ToString(), GuideId.ToString()};
              return csvValues;
            throw new NotImplementedException();
        }




    }
}
