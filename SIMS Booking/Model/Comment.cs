using SIMS_Booking.Enums;
using SIMS_Booking.Utility;
using SIMS_Booking.Utility.Serializer;
using System;

namespace SIMS_Booking.Model
{
    public class Comment: IDable, ISerializable
    {
        private int _id;
        public User Commenter { get; set; }
        public int CommenterId { get; set; }
        public string Text { get; set; }
        public DateTime DateOfPublishing { get; set; }
        public CommentOwnerStatus OwnerStatus { get; set; }
        public int ForumId { get; set; }
        public int NumberOfReports { get; set; }

        public Comment() { }

        public Comment(User commenter, string text, DateTime dateOfPublishing, CommentOwnerStatus ownerStatus, int forumId)
        {
            Commenter = commenter;
            CommenterId = Commenter.GetId();
            Text = text;
            DateOfPublishing = dateOfPublishing;
            OwnerStatus = ownerStatus;
            ForumId = forumId;
            NumberOfReports = 0;
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
            string[] csvValues = { _id.ToString(), Commenter.GetId().ToString(), Text, DateOfPublishing.ToShortDateString(), OwnerStatus.ToString(), ForumId.ToString(), NumberOfReports.ToString() };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            _id = int.Parse(values[0]);
            CommenterId = int.Parse(values[1]);
            Text = values[2];
            DateOfPublishing = DateTime.Parse(values[3]);
            OwnerStatus = (CommentOwnerStatus)Enum.Parse(typeof(CommentOwnerStatus), values[4]);
            ForumId = int.Parse(values[5]);
            NumberOfReports = int.Parse(values[6]);
        }
    }
}
