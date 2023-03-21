using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMS_Booking.Serializer;
using SIMS_Booking.State;

namespace SIMS_Booking.Model
{
    
    public class ConfirmTour :  ISerializable, IDable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int IdTour { get; set; }
        public int IdCheckpoint { get; set; }
        
        public ConfirmTour() { }

        public ConfirmTour(int id, string name, int idTour, int idCheckpoint)
        {
            Id = id;
            Name = name;
            IdTour = idTour;
            IdCheckpoint = idCheckpoint;

            
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Name, IdTour.ToString(), IdCheckpoint.ToString() };

            return csvValues;
        }

        public void FromCSV(string[] values)
        {

            Id = int.Parse(values[0]);
            Name = values[1];
            IdTour = int.Parse(values[2]);
            IdCheckpoint = int.Parse(values[3]);
        }

        public int getID()
        {
            return Id;
        }

        public void setID(int id)
        {
            Id = id;
        }
    }
}
