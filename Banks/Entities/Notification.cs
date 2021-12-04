namespace Banks.Entities
{
    public class Notification
    {
        public Notification(string text)
        {
            Text = text;
        }

        public string Text { get; }
    }
}