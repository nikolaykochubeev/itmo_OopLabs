using System;
using System.Collections.Generic;
using System.Linq;
using Isu.Entities;
using IsuExtra.Tools;

namespace IsuExtra.Entities
{
    public class OgnpCourse
    {
        private readonly List<GroupExtended> _groups = new ();
        public OgnpCourse(string ognpCourseName, string prefix)
        {
            Id = Guid.NewGuid();
            Name = ognpCourseName;
            Prefix = prefix;
        }

        public IReadOnlyList<GroupExtended> Groups => _groups;
        public Guid Id { get; }
        public string Name { get; }
        public string Prefix { get; }
        public GroupExtended AddGroup(Group group)
        {
            if (_groups.FirstOrDefault(extended => extended.Group.GroupName == group.GroupName) is not null)
                throw new IsuExtraException(" ");
            _groups.Add(new GroupExtended(group, Prefix, new Schedule()));
            return _groups.Last();
        }
    }
}