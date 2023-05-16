using System.Collections.Generic;
using System.Linq;
using SIMS_Booking.Utility;
using SIMS_Booking.Utility.Serializer;

namespace SIMS_Booking.Model
{
    public class OwnerReview: ISerializable, IDable
    {
        private int _id;
        public int OwnersCorrectness { get; set; }
        public int Tidiness { get; set; }
        public string Comment { get; set; }
        public Reservation Reservation { get; set; }
        public int ReservationId { get; set; }
        public List<string> ImageURLs { get; set; }
        public bool HasRenovation { get; set; }
        public int RenovationLevel { get; set; }
        public string RenovationComment { get; set; }

        public OwnerReview()
        {
            ImageURLs = new List<string>();
        }
        public OwnerReview(int tidiness, int ownersCorrectness, string comment, Reservation reservation, List<string> imageURLs, bool hasRenovation, int renovationLevel, string renovationComment)
        {
            OwnersCorrectness = ownersCorrectness;
            Tidiness = tidiness;
            Comment = comment;
            Reservation = reservation;
            ReservationId = reservation.GetId();
            ImageURLs = new List<string>();
            foreach (string image in imageURLs)
            {
                ImageURLs.Add(image);
            }
            HasRenovation = hasRenovation;
            RenovationLevel = renovationLevel;
            RenovationComment = renovationComment;
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
            Tidiness = int.Parse(values[1]);
            OwnersCorrectness = int.Parse(values[2]);
            Comment = values[3];
            ReservationId = int.Parse(values[4]);
            ImageURLs = values[5].Split(',').ToList();
            HasRenovation = values[6] == "True";
            RenovationLevel = int.Parse(values[7]);
            RenovationComment = values[8];
        }

        public string[] ToCSV()
        {
            string[] csvValues = { _id.ToString(), Tidiness.ToString(), OwnersCorrectness.ToString(), Comment, ReservationId.ToString(), string.Join(',', ImageURLs), HasRenovation.ToString(), RenovationLevel.ToString(), RenovationComment};
            return csvValues;
        }
    }
}
