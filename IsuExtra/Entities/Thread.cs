using System;
using Isu.Entities;

namespace IsuExtra.Entities
{
    public class Thread
    {
        public Thread(string name, string prefix, Schedule schedule, int maxNumberOfStudents, Group group = null)
        {
            Name = name;
            Prefix = prefix;
            Schedule = schedule;
            Group = @group ?? new Group(name, maxNumberOfStudents, prefix);
        }

        public string Name { get; }
        public string Prefix { get; }
        public Schedule Schedule { get; private set; }
        public Group Group { get; }

        public void ChangeSchedule(Schedule schedule)
        {
            Schedule = schedule;
        }
    }
}