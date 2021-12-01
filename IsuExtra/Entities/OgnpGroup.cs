using System;
using System.Collections.Generic;
using Isu.Entities;
using Isu.Tools;
using IsuExtra.Tools;

namespace IsuExtra.Entities
{
    public class OgnpGroup
    {
        private readonly List<OgnpStudent> _students = new List<OgnpStudent>();
        private List<Lesson> _lessons = new List<Lesson>();
        public OgnpGroup(string name, int max, Ognp ognp)
        {
            Max = max;
            Name = name;
            Oognp = ognp;
        }

        public List<OgnpStudent> Students => _students;
        public Ognp Oognp { get; set; }
        public List<Lesson> Lessons => _lessons;
        public int Max { get; set; }

        public string Name { get; set; }
        public void AddStudent(OgnpStudent st)
        {
            if (_students.Count >= Max)
                throw new GroupIsOvercrowdedException("OgnpGroup is full, can't add more students");
            _students.Add(st);
        }

        public void AddLesson(int chamber, DateTime d, string teacher)
        {
            var l = new Lesson(chamber, d, teacher);
            _lessons.Add(l);
        }
    }
}