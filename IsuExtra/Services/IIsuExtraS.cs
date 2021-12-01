using System;
using System.Collections.Generic;
using Isu.Entities;
using IsuExtra.Entities;

namespace IsuExtra.Services
{
    public interface IIsuExtraS
    {
        Faculty AddFaculty(char name);
        Ognp AddOgnp(Subject s1, Subject s2, string name, Faculty fac);
        public bool ScheduleCheck(MainGroup group, OgnpGroup ognpGroup);
        public void AddStudentToOgnp(OgnpStudent st, OgnpGroup group);
        public void DeleteStudentFromOgnp(OgnpStudent st, OgnpGroup group);
        public List<OgnpStudent> GetStudentsOGNP(OgnpGroup group);
        public List<OgnpStudent> GetStudentsNoOgnp(MainGroup group);
        public List<OgnpStudent> GetStudentsFromNumberOfGroup(int n, Subject subj);
        public MainGroup AddGroup(string name, int max);
        public MainGroup FindGroup(MainGroup groupName);
        public Student AddStudent(MainGroup group, string name);
        public Subject AddSubject(string name);
        public OgnpGroup AddOgnpGroup(string name, int max, Ognp ognp);
        public OgnpStudent FindStudent(string name);
        public Ognp FindOgnp(Faculty fac);
        public Faculty FindFaculty(char name);
    }
}