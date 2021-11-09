using System;

namespace IsuExtra.Entities
{
    public class Lesson
    {
        public Lesson(string name, Audience audience, DayOfWeek dayOfWeek, TimeSpan beginTime, TimeSpan endTime)
        {
            Name = name;
            Audience = audience;
            DayOfWeek = dayOfWeek;
            BeginTime = beginTime;
            EndTime = endTime;
        }

        public string Name { get; }
        public Audience Audience { get; }
        public DayOfWeek DayOfWeek { get; }
        public TimeSpan BeginTime { get; }
        public TimeSpan EndTime { get; }
        public bool CompareLesson(Lesson lesson1)
        {
            if (BeginTime < lesson1.BeginTime)
            {
                return EndTime < lesson1.BeginTime;
            }

            return lesson1.EndTime < BeginTime;
        }
    }
}