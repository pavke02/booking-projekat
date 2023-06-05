using SIMS_Booking.Utility;
using SIMS_Booking.Utility.Serializer;
using System.Collections.Generic;
using System.Linq;

namespace SIMS_Booking.Model
{
    public class Forum: IDable, ISerializable
    {
        private int _id;
        public string Topic { get; set; }
        public User CreatedBy { get; set; }
        public int CreatedById { get; set; }
        public Location Location { get; set; }
        public List<Comment> Comments { get; set; }
        //int -> id owner-a koji ima accommodation na lokaciji
        //bool -> da li je video
        public Dictionary<int, bool> OwnersToNotify { get; set; }
        public int OwnersComments { get; set; }
        public int GuestsComments { get; set; }

        public Forum()
        {
            Comments = new List<Comment>();
        }

        public Forum(User createdBy, Location location, List<Comment> comments, Dictionary<int, bool> ownersToNotify)
        {
            CreatedBy = createdBy;
            CreatedById = CreatedBy.GetId();
            Location = location;
            Comments = comments;
            OwnersToNotify = ownersToNotify;
            Topic = Location.Country + "," + Location.City;
        }

        public int GetId()
        {
            return _id;
        }

        public void SetId(int id)
        {
            _id = id;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { _id.ToString(), CreatedById.ToString(), Location.Country, Location.City, string.Join(',', OwnersToNotify.Select(kvp => string.Join(",", kvp.Key, kvp.Value))) };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            _id = int.Parse(values[0]);
            CreatedById = int.Parse(values[1]);
            Location = new Location(values[2], values[3]);
            Topic = Location.Country + "," + Location.City;
            Comments = new List<Comment>();
            string[] dictionaryItems = values[4].Split(',');
            OwnersToNotify = new Dictionary<int, bool>();
            for (int i = 0; i < dictionaryItems.Length; i += 2)
            {
                OwnersToNotify[int.Parse(dictionaryItems[i])] = bool.Parse(dictionaryItems[i + 1]);
            }
        }
    }
}
