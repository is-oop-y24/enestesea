using System;
using System.Collections.Generic;

namespace IsuExtra.Entities
{
    public class Lesson
    {
        public Lesson(int chamber, DateTime date, string teacher)
        {
            Chamber = chamber;
            Date = date;
            Teacher = teacher;
        }

        public string Teacher { get; set; }

        public DateTime Date { get; set; }

        public int Chamber { get; set; }
    }
}