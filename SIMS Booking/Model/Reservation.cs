using SIMS_Booking.Utility;
using SIMS_Booking.Utility.Serializer;
using System;
using Microsoft.TeamFoundation.Work.WebApi;

namespace SIMS_Booking.Model
{
    public class Reservation : ISerializable, IDable
    {
        private int _id;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Accommodation Accommodation { get; set; }
        public User User { get; set; }
        public bool HasOwnerReviewed { get; set; }
        public bool HasGuestReviewed { get; set; }
        public bool IsCanceled { get; set; }
        public bool IsCancellationReviewed { get; set; }

        public Reservation() { }

        public Reservation(DateTime startDate, DateTime endDate, Accommodation accommodation, User user, bool hasOwnerReviewed, bool hasGuestReviewed, bool isCanceled, bool isCancellationReviewed)
        {
            StartDate = startDate;
            EndDate = endDate;
            Accommodation = accommodation;
            User = user;
            HasOwnerReviewed = hasOwnerReviewed;
            HasGuestReviewed = hasGuestReviewed;
            IsCanceled = isCanceled;
            IsCancellationReviewed = isCancellationReviewed;
        }

        public int GetId()
        {
            return _id;
        }

        public void SetId(int id)
        {
            _id = id;
        }

        public void FromCSV(string[] values)
        {
            _id = int.Parse(values[0]);
            StartDate = DateTime.Parse(values[1]);
            EndDate = DateTime.Parse(values[2]);
            HasOwnerReviewed = bool.Parse(values[3]);
            HasGuestReviewed = bool.Parse(values[4]);
            IsCanceled = bool.Parse(values[5]);
            IsCancellationReviewed = bool.Parse(values[6]);
        }

        public string[] ToCSV()
        {
            string[] csvValues = { _id.ToString(), StartDate.ToShortDateString(), EndDate.ToShortDateString(), HasOwnerReviewed.ToString(), HasGuestReviewed.ToString(), IsCanceled.ToString(), IsCancellationReviewed.ToString()};
            return csvValues;
        }        
    }
}
