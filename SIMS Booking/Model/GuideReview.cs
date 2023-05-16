using System.Collections.Generic;
using System.Linq;
using SIMS_Booking.Model.Relations;
using SIMS_Booking.Utility;
using SIMS_Booking.Utility.Serializer;

namespace SIMS_Booking.Model
{
    public class GuideReview : ISerializable, IDable
    {
        private int ID;
        public int TourRating {get;set;}
        public TourReservation TourReservation { get;set;}
     

        public List<string> ImageURLs { get; set; }


        public GuideReview()
        {
            ImageURLs = new List<string>();
        }
        public GuideReview(int tourRating, TourReservation tourReservation,List<string> imageURLs)
        {
            TourRating = tourRating; 
            TourReservation = tourReservation;
           
            ImageURLs = new List<string>();
            foreach (string image in imageURLs)
            {
                ImageURLs.Add(image);
            }
        }

        public int GetId()
        {
            return ID;
        }

        public void SetId(int id)
        {
            ID = id;
        }

        public void FromCSV(string[] values)
        {
            ID = int.Parse(values[0]);
            TourRating = int.Parse(values[1]);
            ImageURLs = values[2].Split(',').ToList();
        }

        public string[] ToCSV()
        {
            string[] csvValues = { ID.ToString(), TourRating.ToString(), string.Join(',', ImageURLs) };
            return csvValues;
        }


    }
}
