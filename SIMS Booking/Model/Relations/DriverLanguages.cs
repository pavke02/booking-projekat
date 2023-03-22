using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMS_Booking.Observer;
using SIMS_Booking.Serializer;
using SIMS_Booking.Enums;

namespace SIMS_Booking.Model.Relations
{
    public class DriverLanguages : ISerializable
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
    }
}
