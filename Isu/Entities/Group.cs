using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Linq;
using Isu.Tools;

namespace Isu.Entities
{
    public class Group
    {
        private readonly List<Student> _students;
        private string _groupName;
        private int _maxNumberOfStudents;
        private int _numberOfCourse;

        public Group(string groupName, int maxNumberOfStudents)
        {
            _maxNumberOfStudents = maxNumberOfStudents;
            _students = new List<Student>();
            GroupName = groupName;
            _numberOfCourse = Convert.ToInt32(groupName[2]);
            NumberOfCourse = _numberOfCourse;
        }

        public int NumberOfCourse { get; }
        public IReadOnlyList<Student> Students => _students;

        public string GroupName
        {
            get => _groupName;
            private set
            {
                if (!value.StartsWith("M3") || value.Length != 5 ||
                    !char.IsDigit(value[2]) || !char.IsDigit(value[3]) || !char.IsDigit(value[4]))
                    throw new InvalidNameOfGroupException("Invalid name of group");
                _groupName = value;
            }
        }

        public void AddStudent(Student student)
        {
            if (Students.Count >= _maxNumberOfStudents)
                throw new GroupIsOvercrowdedException("Group is full, can't add more students");
            _students.Add(student);
        }

        public void TransferStudent(Group newGroup, Student student)
        {
            if (_students.Count >= _maxNumberOfStudents)
                throw new GroupIsOvercrowdedException("Group is full, can't add more students");
            student.Group._students.Remove(student);
            _students.Add(student);
            student.Transfer(newGroup);
        }
    }
}