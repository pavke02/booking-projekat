﻿using SIMS_Booking.Utility;
using SIMS_Booking.Utility.Serializer;
using System;

namespace SIMS_Booking.Model
{
    public class RenovationAppointment : IDable, ISerializable
    {
        private int Id;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
        public Accommodation Accommodation { get; set; }
        public int AccommodationId {get; set; }
        public bool IsRenovating { get; set;  }

        public RenovationAppointment(DateTime startDate, DateTime endDate, string description, bool isRenovating, Accommodation accommodation)
        {
            StartDate = startDate;
            EndDate = endDate;
            Description = description;
            IsRenovating = isRenovating;
            Accommodation = accommodation;
            AccommodationId = Accommodation.GetId();
        }

        public RenovationAppointment() { }

        public int GetId()
        {
            return Id;
        }

        public void SetId(int id)
        {
            Id = id;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            StartDate = DateTime.Parse(values[1]);
            EndDate = DateTime.Parse(values[2]);
            Description = values[3];
            IsRenovating = bool.Parse(values[4]);
            AccommodationId = int.Parse(values[5]);
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), StartDate.ToShortDateString(), EndDate.ToShortDateString(), Description, IsRenovating.ToString(), AccommodationId.ToString() };
            return csvValues;
        }
    }
}
