﻿using SIMS_Booking.Serializer;
using SIMS_Booking.Utility;

namespace SIMS_Booking.Model.Relations
{
    public class ReservedAccommodation : ISerializable, IDable
    {
        public int UserId { get; set; }
        public int AccommodationId { get; set; }
        public int ReservationId { get; set; }

        public ReservedAccommodation() { }

        public ReservedAccommodation(int userId, int accommodationId, int reservationId)
        {
            UserId = userId;
            AccommodationId = accommodationId;
            ReservationId = reservationId;
        }        

        public void FromCSV(string[] values)
        {
            UserId = int.Parse(values[0]);
            AccommodationId = int.Parse(values[1]);
            ReservationId = int.Parse(values[2]);
        }

        public string[] ToCSV()
        {

            string[] csvValues = { UserId.ToString(), AccommodationId.ToString(), ReservationId.ToString() };
            return csvValues;
        }

        public int getID()
        {
            return UserId;
        }

        public void setID(int id)
        {
            UserId = id;
        }
    }
}
