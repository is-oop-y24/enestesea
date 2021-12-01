using System.Collections.Generic;
using Isu.Entities;
using IsuExtra.Entities;

namespace IsuExtra.Entities
{
public class OgnpStudent : Student
    {
        private List<Ognp> _courses = new List<Ognp>();
        public OgnpStudent(string name, MainGroup group, int id)
        : base(name, group, id)
        {
            Group = group;
        }

        public new MainGroup Group { get;  set; }
        public List<Ognp> Courses => _courses;
        public void AddCourse(Ognp ognp)
        {
            _courses.Add(ognp);
        }
    }
}