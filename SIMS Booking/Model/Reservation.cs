using SIMS_Booking.Serializer;
using SIMS_Booking.State;
using System;

namespace SIMS_Booking.Model
{
    public class Reservation : ISerializable, IDable
    {
        public int ID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Accommodation Accommodation { get; set; }
        public User User { get; set; }

        public Reservation() { }

        public Reservation(DateTime startDate, DateTime endDate, Accommodation accommodation, User user)
        {            
            StartDate = startDate;
            EndDate = endDate;
            Accommodation = accommodation;
            User = user;
        }

        public int getID()
        {
            return ID;
        }

        public void setID(int id)
        {
            ID = id;
        }

        public string[] ToCSV()
        {

            string[] csvValues = { ID.ToString(), StartDate.ToShortDateString(), EndDate.ToShortDateString() };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            ID = int.Parse(values[0]);
            StartDate = DateTime.Parse(values[1]);
            EndDate = DateTime.Parse(values[2]);        
        }
    }
}
