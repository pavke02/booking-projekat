using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIMS_Booking.Serializer;
using SIMS_Booking.State;

namespace SIMS_Booking.Model
{

    

    public class TourPoint: ISerializable , IDable
    {
        public int Id;

        public string Name { get; set; }

        public bool Checked { get; set; }

        public TourPoint() { }
        public TourPoint(int id, string name, bool @checked)
        {
            this.Id = id;
            Name = name;
            Checked = @checked;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Name = values[1];
            Checked = bool.Parse(values[2]);
        }

        public int getID()
        {
            return Id;
        }

        public void setID(int id)
        {
            Id = id;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Name, Checked.ToString() };
            return csvValues;
        }
    }
}
