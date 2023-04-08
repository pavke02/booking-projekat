using System.Collections.Generic;
using System.Linq;
using System.Windows.Documents;
using SIMS_Booking.Serializer;
using SIMS_Booking.Utility;

namespace SIMS_Booking.Model
{
    public class OwnerReview: ISerializable, IDable
    {
        private int ID;
        public int OwnersCorrectness { get; set; }
        public int Tidiness { get; set; }
        public string Comment { get; set; }
        public Reservation Reservation { get; set; }
        public int ReservationId { get; set; }
        public List<string> ImageURLs { get; set; }

        public OwnerReview()
        {
            ImageURLs = new List<string>();
        }
        public OwnerReview(int ownersCorrectness, int tidiness, string comment, Reservation reservation, List<string> imageURLs)
        {
            OwnersCorrectness = ownersCorrectness;
            Tidiness = tidiness;
            Comment = comment;
            Reservation = reservation;
            ReservationId = reservation.getID();
            ImageURLs = new List<string>();
            foreach (string image in imageURLs)
            {
                ImageURLs.Add(image);
            }
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
            OwnersCorrectness = int.Parse(values[1]);
            Tidiness = int.Parse(values[2]);
            Comment = values[3];
            ReservationId = int.Parse(values[4]);
            ImageURLs = values[5].Split(',').ToList();
        }

        public string[] ToCSV()
        {
            string[] csvValues = { ID.ToString(), Tidiness.ToString(), OwnersCorrectness.ToString(), Comment, ReservationId.ToString(), string.Join(',', ImageURLs)};
            return csvValues;
        }
    }
}
