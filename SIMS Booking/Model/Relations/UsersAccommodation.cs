using SIMS_Booking.Utility;
using SIMS_Booking.Utility.Serializer;

namespace SIMS_Booking.Model.Relations
{
    public class UsersAccommodation: ISerializable, IDable
    {
        private int _id;
        public int UserId { get; set; }
        public int AccommodationId { get; set; }

        public UsersAccommodation() { }

        public UsersAccommodation(int userId, int accommodationId)
        {
            UserId = userId;
            AccommodationId = accommodationId;
        }

        public void FromCSV(string[] values)
        {
            _id = int.Parse(values[0]);
            UserId = int.Parse(values[1]);
            AccommodationId = int.Parse(values[2]);            
        }

        public string[] ToCSV()
        {

            string[] csvValues = { _id.ToString(), UserId.ToString(), AccommodationId.ToString() };
            return csvValues;
        }

        public int GetId()
        {
            return _id;
        }

        public void SetId(int id)
        {
            _id = id;
        }
    }
}
