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
                if (!value.StartsWith("M3"))
                    throw new IsuException("Invalid name of group");
                _groupName = value;
            }
        }

        public Student AddStudent(string name, int id)
        {
            if (Students.Count == _maxNumberOfStudents)
                throw new IsuException("Group is full, can't add more students");
            Students.Add(new Student(name, _groupName, id));
            return Students.Last();
        }

        public void TransferStudent(Group oldGroup, Student student)
        {
            if (Students.Count == _maxNumberOfStudents)
                throw new IsuException("Group is full, can't add more students");
            oldGroup.Students.Remove(student);
            Students.Add(student);
            student.Transfer(GroupName);
        }
    }
}