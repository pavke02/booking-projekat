using SIMS_Booking.Utility;
using SIMS_Booking.Utility.Serializer;

namespace SIMS_Booking.Model
{
    public class Voucher : ISerializable, IDable
    {

        public int ID;
        public string Name { get; set; }
        public string TourId { get; set; }
        public string ExpirationTime { get; set; }

        public bool Used { get; set; }

        public Voucher() { }

        public Voucher(string name, string tourId, string expirationTime) { 
        
            Name = name;    
            TourId = tourId;
            ExpirationTime = expirationTime;
        
        }


        public void FromCSV(string[] values)
        {
            ID = int.Parse(values[0]);
            Name = values[1];
            TourId = values[2];
            ExpirationTime= values[3];
            Used = bool.Parse(values[4]);
        }

        public string[] ToCSV()
        {

            string[] csvValues = { ID.ToString(), Name, TourId, ExpirationTime, Used.ToString()};

            return csvValues;
        }

        public int GetId()
        {
           return ID;
        }

        public void SetId(int id)
        {
            ID = id;
        }

       
    }
}
