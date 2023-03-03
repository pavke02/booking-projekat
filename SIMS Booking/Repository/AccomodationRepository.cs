using SIMS_Booking.Model;
using SIMS_Booking.Serializer;
using System.Collections.Generic;

namespace SIMS_Booking.Repository
{
    public class AccomodationRepository
    {
        private const string FilePath = "../../../Resources/Data/accommodations.csv";

        private readonly Serializer<Accommodation> _serializer;
        private List<Accommodation> _accommodations;        

        public AccomodationRepository()
        {
            _serializer = new Serializer<Accommodation>();            
            _accommodations = new List<Accommodation>();
        }

        public List<Accommodation> Load()
        {
            return _serializer.FromCSV(FilePath);
        }

        public void Save(Accommodation accomodation)
        {
            _accommodations.Add(accomodation);
            _serializer.ToCSV(FilePath, _accommodations);
        }
    }
}
