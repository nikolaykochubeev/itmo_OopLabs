using System;

namespace IsuExtra.Entities
{
    public class Pair
    {
        public Pair(string name, Audience audience, DayOfWeek dayOfWeek, TimeSpan time)
        {
            Name = name;
            Audience = audience;
            DayOfWeek = dayOfWeek;
            Time = time;
        }

        public string Name { get; }
        public Audience Audience { get; }
        public DayOfWeek DayOfWeek { get; }
        public TimeSpan Time { get; }

        // TimeSpan interval = new TimeSpan(2, 14, 18);
        // DayOfWeek dayOfWeek = DayOfWeek.Friday;
    }
}