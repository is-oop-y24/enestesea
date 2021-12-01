using System;
using System.Collections.Generic;
using Isu.Entities;

namespace IsuExtra.Entities
{
    public class MainGroup : Group
    {
        private List<Lesson> _lessons = new List<Lesson>();

        public MainGroup(string groupName, int maxNumberOfStudents)
            : base(groupName, maxNumberOfStudents)
        {
            Name = groupName;
            Max = maxNumberOfStudents;
        }

        public int Max { get; }

        public string Name { get; }
        public List<Lesson> Lessons => _lessons;
        public List<Student> NewStudents { get; set; }

        public void AddLesson(int chamber, DateTime d, string teacher)
        {
            var l = new Lesson(chamber, d, teacher);
            Lessons.Add(l);
        }
    }
}