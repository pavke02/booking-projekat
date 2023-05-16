using System;
using SIMS_Booking.Enums;
using SIMS_Booking.Utility;
using SIMS_Booking.Utility.Serializer;

namespace SIMS_Booking.Model.Relations
{
    public class DriverLanguages : ISerializable, IDable
    {
        private int _id;
        public int DriverId { get; set; }
        public Language Language { get; set; }

        public DriverLanguages() { }

        public DriverLanguages(int driverId, Language language)
        {
            DriverId = driverId;
            Language = language;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { _id.ToString(), DriverId.ToString(), Language.ToString()};
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            _id = _id = int.Parse(values[0]);
            DriverId = int.Parse(values[1]);
            Language = (Language)Enum.Parse(typeof(Language), values[2]);
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
