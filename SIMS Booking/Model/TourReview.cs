using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using SIMS_Booking.Serializer;
using SIMS_Booking.State;

namespace SIMS_Booking.Model
{
    public class TourReview: ISerializable, IDable
    {
        public int Id { get; set; }

        public int ConfirmTourId { get; set; }
        public ConfirmTour ConfirmTour { get; set; }
        public double Grade { get; set; }
        public string Description { get; set; }
        public bool IsValid { get; set; }
        


        public TourReview(){}

        public TourReview(int id,int confirmTour, int grade,string description,bool isValid)
        {
            Id = id;
            ConfirmTourId = confirmTour;
            Grade = grade;
            Description = description;
            IsValid = isValid;

        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), ConfirmTourId.ToString(), Description, Grade.ToString(),IsValid.ToString() };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            ConfirmTourId = int.Parse(values[1]);
            Description = values[2];
            Grade = int.Parse(values[3]);
            IsValid = bool.Parse(values[4]);
        }

        public int getID()
        {
           return Id;
        }

        public void setID(int id)
        {
            Id = id;
        }
    }
}
