using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Isu.Tools;

namespace Isu.Entities
{
    public class Group
    {
        private string _groupName;
        private int _maxNumberOfStudents;

        public Group(string groupName, int maxNumberOfStudents)
        {
            _maxNumberOfStudents = maxNumberOfStudents;
            Students = new List<Student>();
            GroupName = groupName;
        }

        public List<Student> Students { get; }

        public string GroupName
        {
            get => _groupName;
            private set
            {
                if (!value.StartsWith("M3") || value.Length != 5 ||
                    char.IsLetter(value[2]) || char.IsLetter(value[3]) || char.IsLetter(value[4]))
                    throw new InvalidNameOfGroupException("Invalid name of group");
                _groupName = value;
            }
        }

        public void AddStudent(Student student)
        {
            if (Students.Count >= _maxNumberOfStudents)
                throw new GroupIsOvercrowdedException("Group is full, can't add more students");
            Students.Add(student);
        }

        public void TransferStudent(Group newGroup, Student student)
        {
            if (Students.Count >= _maxNumberOfStudents)
                throw new GroupIsOvercrowdedException("Group is full, can't add more students");
            student.NameOfGroup.Students.Remove(student);
            Students.Add(student);
            student.Transfer(newGroup);
        }
    }
}