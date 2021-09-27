using System;
using System.Collections.Generic;
using System.Linq;
using Isu.Entities;
using Isu.Tools;

namespace Isu.Services
{
    public class IsuService : IIsuService
    {
        private Dictionary<string, Group> _groups = new ();
        private int _maxNumberOfStudents;
        private int _id;
        public IsuService(int maxNumberOfStudents = 30)
        {
            _maxNumberOfStudents = maxNumberOfStudents;
        }

        public Group AddGroup(string name)
        {
            _groups[name] = new Group(name, _maxNumberOfStudents);
            return _groups[name];
        }

        public Student AddStudent(Group group, string name)
        {
            Student st = new Student(name, group, _id++);
            group.AddStudent(st);
            return st;
        }

        public Student GetStudent(int id)
        {
            foreach (Group group in _groups.Values)
            {
                foreach (Student student in @group.Students)
                if (student.Id == id) return student;
            }

            throw new NoSuchStudentException("There is no student with sich Id");
        }

        public Student FindStudent(string name)
        {
            return _groups.Values.SelectMany(groups => groups.Students).FirstOrDefault(student => student.Name == name);
        }

        public List<Student> FindStudents(string groupName)
        {
            foreach (Group group in _groups.Values)
            {
                if (group.GroupName == groupName) return group.Students;
            }

            return null;
        }

        public List<Student> FindStudents(CourseNumber courseNumber)
        {
            List<Student> all = new ();
            foreach (Group groups in _groups.Values.Where(groups => Convert.ToInt32(groups.GroupName[2]) == courseNumber.Number))
                all.AddRange(groups.Students);
            return all;
        }

        public Group FindGroup(Group groupName)
        {
            return _groups.Values.FirstOrDefault(groups => groups.GroupName == groupName.GroupName);
        }

        public List<Group> FindGroups(CourseNumber courseNumber)
        {
            return _groups.Values.Where(groups => Convert.ToInt32(groups.GroupName[2]) == courseNumber.Number).ToList();
        }

        public void ChangeStudentGroup(Student student, Group newGroup)
        {
           newGroup.TransferStudent(FindGroup(newGroup), student);
        }
    }
}