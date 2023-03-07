using SIMS_Booking.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMS_Booking.Serializer;

namespace SIMS_Booking.Model
{
    public class Reservation : ISerializable, IDable
    {
        public int Id { get; set; }
        public int AccommodationId { get; set; }
        public int UserId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public Reservation()
        {
        }

        public Reservation(int id, int accommodationId, int userId, DateTime startDate, DateTime endDate)
        {
            Id = id;
            AccommodationId = accommodationId;
            UserId = userId;
            StartDate = startDate;
            EndDate = endDate;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(), AccommodationId.ToString(), UserId.ToString(), StartDate.ToShortDateString(),
                EndDate.ToShortDateString()
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            AccommodationId = int.Parse(values[1]);
            UserId = int.Parse(values[2]);
            StartDate = DateTime.Parse(values[3]);
            EndDate = DateTime.Parse(values[4]);
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
