﻿using SIMS_Booking.Observer;
using SIMS_Booking.Serializer;
using SIMS_Booking.Utility;
using System;

namespace SIMS_Booking.Model.Relations
{
    public class TourReservation : ISerializable, IDable
    {

        public int UserId { get; set; }
        public int TourId { get; set; }
        public int Id { get; set; }
        public int NumberOfGuests { get; set; }
        public bool HasGuideReviewed { get; set; }
        public bool HasGuestReviewed { get; set; }

        public TourReservation() { }

        public TourReservation(int userId, int tourId, int numberOfGuests)
        {
            TourId = tourId;
            UserId = userId;
            NumberOfGuests = numberOfGuests;
            
        }


        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            UserId = int.Parse(values[1]);
            TourId = int.Parse(values[2]);
            NumberOfGuests = int.Parse(values[3]);
            HasGuideReviewed = bool.Parse(values[4]);
            HasGuestReviewed = bool.Parse(values[5]);

        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), UserId.ToString(), TourId.ToString(), NumberOfGuests.ToString(), HasGuideReviewed.ToString(), HasGuestReviewed.ToString() };
            return csvValues;
            throw new NotImplementedException();
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

