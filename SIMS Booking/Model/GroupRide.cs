using SIMS_Booking.Enums;
using SIMS_Booking.Model.Relations;
using SIMS_Booking.Model;
using SIMS_Booking.Utility;
using SIMS_Booking.Utility.Serializer;
using System;
using System.Runtime.InteropServices;

namespace SIMS_Booking.Model
{
    public class GroupRide : ISerializable, IDable
    {
        private int ID;

        public int NumberOfPassengers { get; set; }
        public Address StartingAddress { get; set; }
        public Address EndingAddress { get; set; }
        public string TimeOfDeparture { get; set; }

        public string Language { get; set; }

        public GroupRide()
        {
        }

        public GroupRide(int id, int numberOfPassengers, Address startingAddress,Address endingAddress, string timeOfDeparture, string language) {
        
            ID = id;
            NumberOfPassengers = numberOfPassengers;
            StartingAddress = startingAddress;
            EndingAddress = endingAddress;
            TimeOfDeparture = timeOfDeparture;
            Language = language;
        
        }


        public int GetId()
        {
            return ID;
        }

        public void SetId(int id)
        {
            ID = id;
        }

        
        public void FromCSV(string[] values)
        {
            ID = Convert.ToInt32(values[0]);
            NumberOfPassengers = Convert.ToInt32(values[1]);
            StartingAddress = new Address(values[2], new Location(values[3], values[4]));
            EndingAddress = new Address(values[5], new Location(values[6], values[7]));
            TimeOfDeparture= values[8];
            Language = values[9];
        }

        public string[] ToCSV()
        {
            string[] csvValues = { ID.ToString(), NumberOfPassengers.ToString(), StartingAddress.Street, StartingAddress.Location.Country,StartingAddress.Location.City,EndingAddress.Street,EndingAddress.Location.Country,EndingAddress.Location.City,TimeOfDeparture,Language};
            return csvValues;
        }


    }
}
