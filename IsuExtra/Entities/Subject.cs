using System.Collections.Generic;

namespace IsuExtra.Entities
{
    public class Subject
    {
        private List<OgnpGroup> _ognpGroups;

        public Subject(string name)
        {
            _ognpGroups = new List<OgnpGroup>();
            Name = name;
        }

        public string Name { get; set; }

        public List<OgnpGroup> GetOgnpGroups => _ognpGroups;
    }
}