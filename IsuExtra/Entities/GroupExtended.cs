using Isu.Entities;

namespace IsuExtra.Entities
{
    public class GroupExtended
    {
        public GroupExtended(Group group, string prefix, Schedule schedule)
        {
            Group = group;
            Prefix = prefix;
            Schedule = schedule;
        }

        public Group Group { get; }
        public string Prefix { get; }
        public Schedule Schedule { get; }
    }
}