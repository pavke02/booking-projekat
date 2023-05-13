using SIMS_Booking.Utility;
using SIMS_Booking.Utility.Serializer;

namespace SIMS_Booking.Model.Relations
{
    public class UsersAccommodation: ISerializable, IDable
    {
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
            UserId = int.Parse(values[0]);
            AccommodationId = int.Parse(values[1]);            
        }

        public string[] ToCSV()
        {

            string[] csvValues = { UserId.ToString(), AccommodationId.ToString() };
            return csvValues;
        }

        public int GetId()
        {
            return UserId;
        }

        public void SetId(int id)
        {
            UserId = id;
        }
    }
}
