using SIMS_Booking.Serializer;
using SIMS_Booking.Utility;
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
        public bool HasOwnerReviewed { get; set; }
        public bool HasGuestReviewed { get; set; }

        public Reservation() { }

        public Reservation(DateTime startDate, DateTime endDate, Accommodation accommodation, User user, bool hasOwnerReviewed, bool hasGuestReviewed)
        {
            StartDate = startDate;
            EndDate = endDate;
            Accommodation = accommodation;
            User = user;
            HasOwnerReviewed = hasOwnerReviewed;
            HasGuestReviewed = hasGuestReviewed;
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
            HasOwnerReviewed = bool.Parse(values[3]);
            HasGuestReviewed = bool.Parse(values[4]);
        }

        public string[] ToCSV()
        {            
            string[] csvValues = { ID.ToString(), StartDate.ToShortDateString(), EndDate.ToShortDateString(), HasOwnerReviewed.ToString(), HasGuestReviewed.ToString() };
            return csvValues;
        }        
    }
}
