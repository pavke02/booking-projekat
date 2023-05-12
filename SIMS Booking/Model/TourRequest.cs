using System;
using System.Collections.Generic;
using System.Linq;
using SIMS_Booking.UI.View;
using System.Xml.Linq;
using SIMS_Booking.Utility;
using SIMS_Booking.Utility.Serializer;
using SIMS_Booking.Enums;

namespace SIMS_Booking.Model
{
    public class TourRequest : ISerializable, IDable
    {

        private int ID = 1;

        public Requests Requests { get; set; }
        public Location Location { get; set; }
        public string Description { get; set; }
        public string Language { get; set; }
        public int NumberOfGuests { get; set; }
        public DateTime TimeOfStart { get; set; }
        public DateTime TimeOfEnd { get; set; }

        public TourRequest() { }

        public int getID()
        {
            return ID;
        }

        public void setID(int id)
        {
            ID = id;
        }

        void ISerializable.FromCSV(string[] values)
        {
            ID = int.Parse(values[0]);
            Location = new Location(values[1], values[2]);
            Language = values[3];
            NumberOfGuests = int.Parse(values[4]);
            TimeOfStart = DateTime.Parse(values[5]);
            TimeOfEnd = DateTime.Parse(values[6]);
            Requests = (Requests)Enum.Parse(typeof(Requests), values[7]);
        }

        string[] ISerializable.ToCSV()
        {
            string[] csvValues = { ID.ToString(), Location.Country, Location.City, Language.ToString(), NumberOfGuests.ToString(), TimeOfStart.ToString(), TimeOfEnd.ToString(), Requests.ToString() };

            return csvValues;
        }





    }
}
