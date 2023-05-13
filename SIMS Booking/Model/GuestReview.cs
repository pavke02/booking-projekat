 using SIMS_Booking.Utility;
using SIMS_Booking.Utility.Serializer;

namespace SIMS_Booking.Model
{
    public class GuestReview: ISerializable, IDable
    {
        private int _id;
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
            ReservationId = reservation.GetId();
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
            RuleFollowing = int.Parse(values[1]);
            Tidiness = int.Parse(values[2]);
            Comment = values[3];   
            ReservationId = int.Parse(values[4]);
        }       

        public string[] ToCSV()
        {
            string[] csvValues = { _id.ToString(), RuleFollowing.ToString(), Tidiness.ToString(), Comment, ReservationId.ToString() };
            return csvValues;
        }
    }
}
