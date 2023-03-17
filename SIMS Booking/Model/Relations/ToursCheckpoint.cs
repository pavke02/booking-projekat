using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMS_Booking.Serializer;

namespace SIMS_Booking.Model.Relations
{
    public class ToursCheckpoint : ISerializable
    {

        public int TourId { get; set; }
        public int CheckpointId { get; set; }

        public ToursCheckpoint() { }

        public ToursCheckpoint(int Id, int cpId)
        {
            TourId = Id;
            CheckpointId = cpId;
           
        }

        public string[] ToCSV()
        {

            string[] csvValues = { TourId.ToString(), CheckpointId.ToString() };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            TourId = int.Parse(values[0]);
            CheckpointId = int.Parse(values[1]);
          
        }

    }
}
