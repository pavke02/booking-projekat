using SIMS_Booking.Model.Relations;
using SIMS_Booking.Utility;
using SIMS_Booking.Utility.Serializer;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;


namespace SIMS_Booking.Model
{
    public class VehicleReservation : ISerializable, IDable
    {
        private int ID;
        public Address StartingAddress { get; set; }
        public Address EndingAddress { get; set; }
        public string TimeOfDeparture { get; set; }


        private Regex _timeRegex = new Regex("^[0 - 2][0 - 3]:[0 - 5][0 - 9]$");


        public int GetId()
        {
            return ID;
        }

        public void SetId(int id)
        {
           ID= id;

        }

        public void FromCSV(string[] values)
        {
            ID = int.Parse(values[0]);
            StartingAddress =new Address(values[1], new Location(values[2], values[3]));
            EndingAddress = new Address(values[4], new Location(values[5], values[6]));
            TimeOfDeparture = values[7];
        }

        public string[] ToCSV()
        {
            string[] csValues = { ID.ToString(), StartingAddress.Street, StartingAddress.Location.Country, StartingAddress.Location.City, EndingAddress.Street, EndingAddress.Location.Country, EndingAddress.Location.City, TimeOfDeparture };
            return csValues;

        }
    }
}
