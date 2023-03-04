using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMS_Booking.Model;
using SIMS_Booking.Serializer;

namespace SIMS_Booking.Repository
{
    public class GuideRepository
    {
        private const string FilePath = "../../../Resources/Data/guides.csv";

        private readonly Serializer<Guide> _serializer;

        private List<Guide> _guides;

        public GuideRepository()
        {
            _serializer = new Serializer<Guide>();
            _guides = _serializer.FromCSV(FilePath);
        }

       
    }
}
