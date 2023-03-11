using SIMS_Booking.Serializer;
using SIMS_Booking.State;
using System;

namespace SIMS_Booking.Model
{
    public class Reservation : ISerializable, IDable
    {
        private int ID;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Accommodation Accommodation { get; set; }
        public User User { get; set; }
        public bool IsReviewed { get; set; }

        public Reservation() { }

        public Reservation(DateTime startDate, DateTime endDate, Accommodation accommodation, User user, bool isReviewed)
        {
            StartDate = startDate;
            EndDate = endDate;
            Accommodation = accommodation;
            User = user;
            IsReviewed = isReviewed;
        }

        public int getID()
        {
            return ID;
        }

        public void setID(int id)
        {
            ID = id;
        }

        public void FromCSV(string[] values)
        {
            ID = int.Parse(values[0]);
            StartDate = DateTime.Parse(values[1]);
            EndDate = DateTime.Parse(values[2]);
            IsReviewed = bool.Parse(values[3]);
        }

        public string[] ToCSV()
        {            
            string[] csvValues = { ID.ToString(), StartDate.ToShortDateString(), EndDate.ToShortDateString(), IsReviewed.ToString() };
            return csvValues;
        }        
    }
}
