using System;
using SIMS_Booking.Enums;
using SIMS_Booking.Utility;
using SIMS_Booking.Utility.Serializer;

namespace SIMS_Booking.Model
{


    public class User : ISerializable, IDable
    {
        private int ID;        
        public string Username { get; set; }
        public string Password { get; set; }
        public Roles Role { get; set; }
        public bool IsSuperUser { get; set; }
        public uint Age { get; set; }

        public User() { }

        public User(string username, string password, Roles role, bool isSuperUser, uint year)
        {
            Username = username;
            Password = password;
            Role = role;
            IsSuperUser = isSuperUser;
            Age = year;
        }

        public int getID()
        {
            return ID;
        }

        public void setID(int id)
        {
            ID = id;
        }

        public void FromCSV(string[] values)
        {
            ID = int.Parse(values[0]);
            Username = values[1];
            Password = values[2];
            Role = (Roles)Enum.Parse(typeof(Roles), values[3]);
            IsSuperUser = bool.Parse(values[4]);
            Age = uint.Parse(values[5]);
        }

        public string[] ToCSV()
        {
            string[] csvValues = { ID.ToString(), Username, Password, Role.ToString(), IsSuperUser.ToString(), Age.ToString() };
            return csvValues;
        }                
    }
}
