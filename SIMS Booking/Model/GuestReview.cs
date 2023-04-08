using SIMS_Booking.Serializer;
using SIMS_Booking.Utility;

namespace SIMS_Booking.Model
{
    public class GuestReview: ISerializable, IDable
    {
        private int ID;
        public int RuleFollowing { get; set; }
        public int Tidiness { get; set; }
        public string Comment { get; set; }
        public  Reservation Reservation { get; set; }
        public int ReservationId { get; set; }

        public GuestReview() { }   
        public GuestReview(int ruleFollowing, int tidiness, string comment, Reservation reservation)
        {
            RuleFollowing = ruleFollowing;
            Tidiness = tidiness;
            Comment = comment;
            Reservation = reservation;
            ReservationId = reservation.getID();
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
            RuleFollowing = int.Parse(values[1]);
            Tidiness = int.Parse(values[2]);
            Comment = values[3];   
            ReservationId = int.Parse(values[4]);
        }       

        public string[] ToCSV()
        {
            string[] csvValues = { ID.ToString(), RuleFollowing.ToString(), Tidiness.ToString(), Comment, ReservationId.ToString() };
            return csvValues;
        }
    }
}
