using SIMS_Booking.Utility;
using SIMS_Booking.Utility.Serializer;

namespace SIMS_Booking.Model
{
    public class TourReview: ISerializable, IDable
    {
        private int Id { get; set; }
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

        public int GetId()
        {
           return Id;
        }

        public void SetId(int id)
        {
            Id = id;
        }
    }
}
