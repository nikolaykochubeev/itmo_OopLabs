using System;

namespace IsuExtra.Entities
{
    public class OgnpCourse
    {
        public OgnpCourse(Guid id, string name, string prefix)
        {
            Id = id;
            Name = name;
            Prefix = prefix;
        }

        public Guid Id { get; }
        public string Name { get; }
        public string Prefix { get; }
    }
}