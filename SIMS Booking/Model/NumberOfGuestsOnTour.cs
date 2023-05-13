using SIMS_Booking.Utility;
using SIMS_Booking.Utility.Serializer;

namespace SIMS_Booking.Model
{
    public class NumberOfGuestsOnTour:ISerializable,IDable
    {
        public int Id { get; set; }
        public int NumberOfGuests { get; set; }
    
        public NumberOfGuestsOnTour() { }

        public NumberOfGuestsOnTour(int id, int numberOfGuests)
        {
            Id = id;
            NumberOfGuests = numberOfGuests;
        }

        public void FromCSV(string[] values)
        {
           Id = int.Parse(values[0]);
           NumberOfGuests = int.Parse(values[1]);
        }
        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), NumberOfGuests.ToString() };

            return csvValues;
        }

        public int GetId()
        {
            return Id;
        }

        public void SetId(int id)
        {
            Id = id;
        }

    }
}
