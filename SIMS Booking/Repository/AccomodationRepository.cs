using SIMS_Booking.Model;
using SIMS_Booking.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIMS_Booking.Repository
{
    public class AccomodationRepository
    {
        private const string FilePath = "../../../Resources/Data/accommodations.csv";

        private readonly Serializer<Accommodation> _serializer;

        public List<Accommodation> Accommodations { get; set; }

        public AccomodationRepository()
        {
            _serializer = new Serializer<Accommodation>();
            Accommodations = _serializer.FromCSV(FilePath);
        }


    }
}
