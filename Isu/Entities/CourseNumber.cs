using System.Collections.Generic;
using System.Net.Sockets;
using Isu.Tools;
namespace Isu.Entities
{
    public class CourseNumber
    {
        private int _number;
        public CourseNumber(int number)
        {
            Number = number;
        }

        public int Number
        {
            get => _number;
            private set
            {
                if (Number is > 5 or < 1)
                    throw new InvalidNumberOfCourseException("Invalid number of course");
                _number = value;
            }
        }
    }
}