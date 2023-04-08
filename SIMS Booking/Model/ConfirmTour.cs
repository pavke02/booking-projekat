using SIMS_Booking.Serializer;
using SIMS_Booking.Utility;

namespace SIMS_Booking.Model
{
    
    public class ConfirmTour :  ISerializable, IDable
    {
        public int Id { get; set; }
        public int IdTour { get; set; }
        public int IdCheckpoint { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

        public ConfirmTour() { }

        public ConfirmTour(int id, string name, int idTour, int idCheckpoint , int userId)
        {
            Id = id;
            IdTour = idTour;
            IdCheckpoint = idCheckpoint;
            UserId = userId;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(),UserId.ToString() ,IdTour.ToString(), IdCheckpoint.ToString() };

            return csvValues;
        }

        public void FromCSV(string[] values)
        {

            Id = int.Parse(values[0]);
            UserId = int.Parse(values[1]);
            IdTour = int.Parse(values[2]);
            IdCheckpoint = int.Parse(values[3]);
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
