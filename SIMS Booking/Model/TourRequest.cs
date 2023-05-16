using System;
using SIMS_Booking.Utility;
using SIMS_Booking.Utility.Serializer;
using SIMS_Booking.Enums;

namespace SIMS_Booking.Model
{
    public class TourRequest : ISerializable, IDable
    {

        public int ID = 1;
        public Requests Requests { get; set; }
        public Location Location { get; set; }
        public string Description { get; set; }
        public string Language { get; set; }
        public int NumberOfGuests { get; set; }
        public DateTime TimeOfStart { get; set; }
        public DateTime TimeOfEnd { get; set; }
        public DateTime DefaultDate { get; set; }
        public DateTime DateOfSendRequest { get; set; }

        public TourRequest() { }

        public TourRequest(int id, Location location, string description, string langugage, int guests, DateTime timeOfStart, DateTime timeOfEnd) {
        
            ID=id;         
            Location = location;
            Description = description;
            Language = langugage;
            NumberOfGuests = guests;
            TimeOfStart = timeOfStart;
            TimeOfEnd = timeOfEnd;
        }

        public int GetId()
        {
            return ID;
        }

        public void SetId(int id)
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
            DefaultDate = DateTime.Parse(values[8]);
            DateOfSendRequest = DateTime.Parse(values[9]);
            Description = values[10];
            

        }

        string[] ISerializable.ToCSV()
        {

            string[] csvValues = { ID.ToString(), Location.Country, Location.City, Language.ToString(), NumberOfGuests.ToString(), TimeOfStart.ToString(), TimeOfEnd.ToString(), Requests.ToString(), DefaultDate.ToString(), DateOfSendRequest.ToString(), Description };
           
            return csvValues;
        }


    }
}
