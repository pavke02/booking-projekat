using SIMS_Booking.Utility;
using SIMS_Booking.Utility.Serializer;

namespace SIMS_Booking.Model
{

    public class ConfirmTour  :  ISerializable, IDable
    {
        public int Id { get; set; }
        public int IdTour { get; set; }
        public int IdCheckpoint { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int Vaucer { get; set; } = 0;

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
            string[] csvValues = { Id.ToString(),UserId.ToString() ,IdTour.ToString(), IdCheckpoint.ToString(), Vaucer.ToString() };

            return csvValues;
        }

        public void FromCSV(string[] values)
        {

            Id = int.Parse(values[0]);
            UserId = int.Parse(values[1]);
            IdTour = int.Parse(values[2]);
            IdCheckpoint = int.Parse(values[3]);
            Vaucer = int.Parse(values[4]);
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
