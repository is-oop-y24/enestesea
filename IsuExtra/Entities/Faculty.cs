using System.Collections.Generic;
using IsuExtra.Tools;

namespace IsuExtra.Entities
{
    public class Faculty
    {
        public Faculty(char name)
        {
            Name = name;
            Groups = new List<MainGroup>();
        }

        public Ognp Oognp { get; set; }

        public List<MainGroup> Groups { get; set; }
        public List<OgnpGroup> OgnpGroups { get; set; }

        public char Name { get; set; }
        public void AddOgnp(Ognp ognp)
        {
            if (Oognp != null)
                throw new IsuExtraException("Уже есть огнп у этого мегафакультета");
            Oognp = ognp;
        }
    }
}