using System;
using Isu.Tools;
namespace Isu.Entities
{
    public class Student : IEquatable<Student>
    {
        public Student(string name, string groupName, int id)
        {
            Id = id;
            Name = name;
            GroupName = groupName;
        }

        public int Id { get; }
        public string Name { get; }
        public string GroupName { get; private set; }

        public void Transfer(string newGroup)
        {
            GroupName = newGroup;
        }

        public bool Equals(Student other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id && Name == other.Name && GroupName == other.GroupName;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Student)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name, GroupName);
        }
    }
}