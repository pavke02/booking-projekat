using System;
using SIMS_Booking.Enums;
using SIMS_Booking.Utility;
using SIMS_Booking.Utility.Serializer;

namespace SIMS_Booking.Model.Relations
{
    public class DriverLanguages : ISerializable, IDable
    {
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
            string[] csvValues = { DriverId.ToString(), Language.ToString()};
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            DriverId = int.Parse(values[0]);
            Language = (Language)Enum.Parse(typeof(Language), values[1]);
        }

        public int GetId()
        {
            return DriverId;
        }

        public void SetId(int id)
        {
            DriverId = id;
        }
    }
}
