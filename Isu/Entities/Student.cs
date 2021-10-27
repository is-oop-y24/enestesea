using System;
using Isu.Tools;
namespace Isu.Entities
{
    public class Student : IEquatable<Student>
    {
        private Group _group = null;
        public Student(string name, Group @group, int id)
        {
            Id = id;
            Name = name;
            Group = @group;
        }

        public int Id { get; }
        public string Name { get; }
        public Group Group
        {
            get => _group;
            set
            {
                _group = value;
            }
        }

        public void Transfer(Group newGroup)
        {
            Group = newGroup;
        }

        public bool Equals(Student other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id && Name == other.Name && Group == other.Group;
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
            return HashCode.Combine(Id, Name, Group);
        }
    }
}