using System.Reflection;

namespace IsuExtra.Entities
{
    public class Ognp
    {
        public Ognp(Subject subject1, Subject subject2, string name, Faculty faculty)
        {
            Subject2 = subject1;
            Subject1 = subject1;
            Name = name;
            Fac = faculty;
        }

        public Faculty Fac { get; set; }

        public string Name { get; set; }

        public Subject Subject1 { get; set; }

        public Subject Subject2 { get; set; }
    }
}