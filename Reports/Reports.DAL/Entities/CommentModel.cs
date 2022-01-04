using System;

namespace Reports.DAL.Entities
{
    public class Comment
    {
        private Comment()
        {
        }
        public Comment(string text, DateTime dateTime)
        {
            Id = Guid.NewGuid();
            Text = text;
            ChangeTime = dateTime;
        }

        public Comment(string text)
        {
            Id = Guid.NewGuid();
            Text = text;
            ChangeTime = DateTime.Now;
        }

        public Guid Id { get; set; }
        public Guid TaskId { get; set; }
        public string Text { get; set; }
        public DateTime ChangeTime { get; set; }
    }
}