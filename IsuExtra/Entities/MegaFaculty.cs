using System;

namespace IsuExtra.Entities
{
    public class MegaFaculty
    {
        public MegaFaculty(Guid id, string name, string prefix)
        {
            Id = id;
            Name = name;
            Prefix = prefix;
        }

        public Guid Id { get; }
        public string Name { get; }

        // public OgnpCourse OgnpCourse { get; private set; }
        public string Prefix { get; }

        // public OgnpCourse AddOgnpCourse(string name)
        // {
        //     OgnpCourse = new OgnpCourse(Id, name, Prefix);
        //     return OgnpCourse;
        // }
    }
}