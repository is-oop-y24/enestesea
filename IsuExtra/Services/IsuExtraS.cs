using System;
using System.Collections.Generic;
using System.Linq;
using Isu.Entities;
using IsuExtra.Entities;
using IsuExtra.Tools;

namespace IsuExtra.Services
{
    public class IsuExtraS : IIsuExtraS
    {
        private int _id;
        private Dictionary<string, MainGroup> _groups = new ();
        private List<Faculty> _facs = new List<Faculty>();
        private Dictionary<string, OgnpGroup> _ognpGroups = new ();

        public Faculty AddFaculty(char name)
        {
            var fac = new Faculty(name);
            _facs.Add(fac);
            return fac;
        }

        public Ognp AddOgnp(Subject s1, Subject s2, string name, Faculty fac)
        {
            var ognp = new Ognp(s1, s2, name, fac);
            fac.AddOgnp(ognp);
            return ognp;
        }

        public bool ScheduleCheck(MainGroup group, OgnpGroup ognpGroup)
        {
            return @group.Lessons.All(lesson => ognpGroup.Lessons.Find(l => l.Date == lesson.Date) == null);
        }

        public void AddStudentToOgnp(OgnpStudent st, OgnpGroup group)
        {
            if (group.Oognp.Fac.Name != st.Group.GroupName[0])
            {
                    if (st.Courses.Count < 2)
                    {
                        if (ScheduleCheck(st.Group, group))
                        {
                            group.AddStudent(st);
                            st.AddCourse(group.Oognp);
                        }
                        else
                        {
                            throw new ScheduleConflictsException("Group lessons are in the same time as OGNP lessons");
                        }
                    }
                    else
                    {
                        throw new StudentAlreadyHave2CoursesException("Student is already have 2 OGNP");
                    }
            }
            else
            {
                    throw new OgnpSameFacultyException("Faculty = OGNP faculty");
            }
        }

        public void DeleteStudentFromOgnp(OgnpStudent st, OgnpGroup group)
        {
            group.Students.Remove(st);
            st.Courses.Remove(group.Oognp);
        }

        public List<OgnpStudent> GetStudentsOGNP(OgnpGroup group)
        {
            return group.Students;
        }

        public List<OgnpStudent> GetStudentsNoOgnp(MainGroup group)
        {
            var list = new List<OgnpStudent>();
            foreach (OgnpStudent st in group.Students)
            {
                if (st.Courses.Count == 0)
                {
                    list.Add(st);
                }
            }

            return list;
        }

        public List<OgnpStudent> GetStudentsFromNumberOfGroup(int n, Subject subj)
        {
            if (subj == null) throw new IsuExtraException("No subject");
            return subj.GetOgnpGroups[n - 1].Students.ToList();
        }

        public MainGroup AddGroup(string name, int max)
        {
            _groups[name] = new MainGroup(name, max);
            return _groups[name];
        }

        public MainGroup FindGroup(MainGroup groupName)
        {
            return _groups.Values.FirstOrDefault(groups => groups.GroupName == groupName.GroupName);
        }

        public Student AddStudent(MainGroup group, string name)
        {
            OgnpStudent st = new OgnpStudent(name, group, _id++);
            group.AddStudent(st);
            return st;
        }

        public Subject AddSubject(string name)
        {
            Subject subj = new Subject(name);
            return subj;
        }

        public OgnpGroup AddOgnpGroup(string name, int max, Ognp ognp)
        {
            OgnpGroup group = new OgnpGroup(name, max, ognp);
            _ognpGroups[name] = group;
            return _ognpGroups[name];
        }

        public OgnpStudent FindStudent(string name)
        {
            return (OgnpStudent)_groups.Values.SelectMany(groups => groups.Students).FirstOrDefault(student => student.Name == name);
        }

        public Ognp FindOgnp(Faculty fac)
        {
            return _facs.Where(f => f.Name == fac.Name).Select(f => f.Oognp).FirstOrDefault();
        }

        public Faculty FindFaculty(char name)
        {
            return _facs.FirstOrDefault(f => f.Name == name);
        }
    }
}
