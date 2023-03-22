﻿using System;
using SIMS_Booking.Serializer;
using SIMS_Booking.Enums;
using SIMS_Booking.State;

namespace SIMS_Booking.Model
{


    public class User : ISerializable, IDable
    {
        private int ID;        
        public string Username { get; set; }
        public string Password { get; set; }
        public Roles Role { get; set; }

        public User() { }

        public User(string username, string password)
        {
            Username = username;
            Password = password;
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
        }

        public string[] ToCSV()
        {
            string[] csvValues = { ID.ToString(), Username, Password, Role.ToString() };
            return csvValues;
        }                
    }
}
