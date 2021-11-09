using System;
using System.Collections.Generic;
using System.Linq;
using IsuExtra.Tools;

namespace IsuExtra.Entities
{
    public class Schedule
    {
        private readonly List<Lesson> _lessons;
        public Schedule(List<Lesson> pairs = null)
        {
            _lessons = pairs ?? new List<Lesson>();
        }

        public IReadOnlyList<Lesson> Lessons => _lessons;

        public void AddLesson(Lesson lesson)
        {
            if (_lessons.FirstOrDefault(lesson1 => lesson1.DayOfWeek == lesson.DayOfWeek && lesson.CompareLesson(lesson1))
                is not null)
            {
                throw new IsuExtraException("Lesson overlap");
            }

            _lessons.Add(lesson);
        }

        public void RemoveLesson(Lesson lesson)
        {
            if (!_lessons.Contains(lesson))
            {
                throw new IsuExtraException("Lesson is doesn't exist");
            }

            _lessons.Remove(lesson);
        }

        public bool InvarianceIntersectionCheck(List<Lesson> lessons)
        {
            return _lessons.All(lesson => lessons.FirstOrDefault(lesson1 => lesson1.CompareLesson(lesson)) is null);
        }
    }
}