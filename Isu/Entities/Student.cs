using System;
using Isu.Tools;
namespace Isu.Entities
{
    public class Student : IEquatable<Student>
    {
        private Group _nameOfGroup = null;
        public Student(string name, Group nameOfGroup, int id)
        {
            Id = id;
            Name = name;
            NameOfGroup = nameOfGroup;
        }

        public int Id { get; }
        public string Name { get; }
        public Group NameOfGroup
        {
            get => _nameOfGroup;
            set
            {
                _nameOfGroup = value;
            }
        }

        public void Transfer(Group newGroup)
        {
            NameOfGroup = newGroup;
        }

        public bool Equals(Student other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id && Name == other.Name && NameOfGroup == other.NameOfGroup;
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
            return HashCode.Combine(Id, Name, NameOfGroup);
        }
    }
}