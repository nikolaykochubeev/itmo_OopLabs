﻿using System;
using System.Collections.Generic;
using System.Linq;
using Isu.Tools;

namespace Isu.Entities
{
    public class Group
    {
        private readonly List<Student> _students;
        public Group(string groupName, int maximumNumberOfStudents)
        {
            GroupName = groupName;
            _students = new List<Student>();
            MaximumNumberOfStudents = maximumNumberOfStudents;
        }

        public string GroupName { get; }

        public IReadOnlyList<Student> Students => _students;
        public int MaximumNumberOfStudents { get; }

        public Student AddStudent(Student student)
        {
            if (MaximumNumberOfStudents == Students.Count)
                throw new IsuException("Group is full, student cannot be added");
            _students.Add(student);
            return Students.Last();
        }

        public void RemoveStudent(Guid studentId)
        {
            _students.Remove(GetStudent(studentId));
        }

        public void TransferStudent(Student student, Group oldGroup)
        {
            oldGroup.RemoveStudent(student.Id);
            AddStudent(new Student(student.Id, student.Name, GroupName));
        }

        public Student GetStudent(Guid id)
        {
            return Students.FirstOrDefault(student => student.Id == id);
        }

        public Student GetStudent(string name)
        {
            return Students.FirstOrDefault(student => student.Name == name);
        }
    }
}