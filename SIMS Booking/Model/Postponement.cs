using System;
using SIMS_Booking.Enums;
using SIMS_Booking.Utility;
using SIMS_Booking.Utility.Serializer;

namespace SIMS_Booking.Model
{
    public class Postponement : ISerializable, IDable
    {
        private int _id { get; set; }
        public int ReservationId { get; set; }
        public Reservation Reservation { get; set; }
        public DateTime NewStartDate { get; set; }
        public DateTime NewEndDate { get; set; }
        public PostponementStatus Status { get; set; }
        public string Comment { get; set; }
        public bool IsNotified { get; set; }

        public Postponement()
        {
        }

        public Postponement(Reservation reservation, DateTime newStartDate, DateTime newEndDate, PostponementStatus status, bool isNotified)
        {
            Reservation = reservation;
            ReservationId = reservation.GetId();
            NewStartDate = newStartDate;
            NewEndDate = newEndDate;
            Status = status;
            Comment = "No comment";
            IsNotified = isNotified;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                _id.ToString(), ReservationId.ToString(), NewStartDate.ToShortDateString(),
                NewEndDate.ToShortDateString(), Status.ToString(), Comment, IsNotified.ToString()
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            _id = int.Parse(values[0]);
            ReservationId = int.Parse(values[1]);
            NewStartDate = (DateTime.Parse(values[2]));
            NewEndDate = (DateTime.Parse(values[3]));
            Status = (PostponementStatus)Enum.Parse(typeof(PostponementStatus), values[4]);
            Comment = values[5];
            IsNotified = (values[6] == "True"); 
        }

        public int GetId()
        {
            return _id;
        }

        public void SetId(int id)
        {
            _id = id;
        }
    }
}
