using System;
using System.Globalization;
using Isu.Tools;
using IsuExtra.Entities;
using IsuExtra.Services;
using IsuExtra.Tools;
using NUnit.Framework;

namespace IsuExtra.Tests
{
    public class IsuExtraTest
    {
        private IIsuExtraS _isuExtraS;
        [SetUp]
        public void Setup()
        {
            _isuExtraS = new IsuExtraS();
            var g1 = _isuExtraS.AddGroup("M3204", 30);
            var g2 = _isuExtraS.AddGroup("M3201", 30);
            _isuExtraS.AddStudent(g1, "valera");
            _isuExtraS.AddStudent(g2, "vasya");
            var sub1 = _isuExtraS.AddSubject("Киб");
            var sub2 = _isuExtraS.AddSubject("ПО"); 
            var F1 = _isuExtraS.AddFaculty('M');
            var Ognp1 = _isuExtraS.AddOgnp(sub1, sub2, "Cyber", F1);
            _isuExtraS.AddOgnpGroup("1", 30, Ognp1);
            _isuExtraS.AddOgnpGroup("2", 30, Ognp1);
            var F2 = _isuExtraS.AddFaculty('A');
            var sub3 = _isuExtraS.AddSubject("Meneg");
            var sub4 = _isuExtraS.AddSubject("Buh");
            var Ognp2 = _isuExtraS.AddOgnp(sub3, sub4, "Бизнес", F2);
           
            
            

        }

[Test]
        public void AddStudentInHisFacultyOgnpCourse_ThrowException()
        {
            {
                var F = _isuExtraS.FindFaculty('M');
                var ognp = _isuExtraS.FindOgnp(F);
                var ognpGroup1 = _isuExtraS.AddOgnpGroup("1", 30, ognp);
                Assert.Catch<OgnpSameFacultyException>(() =>_isuExtraS.AddStudentToOgnp(_isuExtraS.FindStudent("valera"), ognpGroup1));
            }
        }

        [Test]
        public void AddStudentInOgnpCourseSameLessonsAsMainGroup_ThrowException()
        {
            {
                var date = Convert.ToDateTime("30/09/2021 10:00:00", new CultureInfo("ru-Ru"));
                var F = _isuExtraS.FindFaculty('A');
                var ognp = _isuExtraS.FindOgnp(F);
                var st = _isuExtraS.FindStudent("vasya");
                var G = _isuExtraS.FindGroup(st.Group);
                var ognpGroup1 = _isuExtraS.AddOgnpGroup("1", 30, ognp); 
                G.AddLesson(303, date, "Vladislav");
                ognpGroup1.AddLesson(420, date, "Anna");
                Assert.Catch<ScheduleConflictsException>(() =>_isuExtraS.AddStudentToOgnp(st,ognpGroup1));
            }  
        }
        [Test]
        public void AddStudentInThreeOgnp_ThrowException()
        {
            {
                var sub1 = _isuExtraS.AddSubject("Киб");
                var sub2 = _isuExtraS.AddSubject("ПО");
                var F1 = _isuExtraS.AddFaculty('A');
                var F2 = _isuExtraS.AddFaculty('K');
                var F3 = _isuExtraS.AddFaculty('L');
                var Ognp1 = _isuExtraS.AddOgnp(sub1, sub2, "Cyber", F1);
                var Ognp2 = _isuExtraS.AddOgnp(sub1, sub2, "Cyber", F2);
                var Ognp3 = _isuExtraS.AddOgnp(sub1, sub2, "Cyber", F3);
                var ognpGroup1 = _isuExtraS.AddOgnpGroup("1", 30, Ognp1);
                var ognpGroup2 = _isuExtraS.AddOgnpGroup("1", 30, Ognp2);
                var ognpGroup3 = _isuExtraS.AddOgnpGroup("1", 30, Ognp3);
                _isuExtraS.AddStudentToOgnp(_isuExtraS.FindStudent("valera"), ognpGroup1);
                _isuExtraS.AddStudentToOgnp(_isuExtraS.FindStudent("valera"), ognpGroup2);
                Assert.Catch<StudentAlreadyHave2CoursesException>(() =>_isuExtraS.AddStudentToOgnp(_isuExtraS.FindStudent("valera"), ognpGroup3));
            }       
        }

        [Test]
        public void RegisterAndDeleteStudentInStream_StudentInNotRegisteredStudentsList()
        {
            var sub1 = _isuExtraS.AddSubject("Киб");
            var sub2 = _isuExtraS.AddSubject("ПО");
            var F1 = _isuExtraS.AddFaculty('L');
            var Ognp1 = _isuExtraS.AddOgnp(sub1, sub2, "Cyber", F1);
            var ognpGroup1 = _isuExtraS.AddOgnpGroup("1", 30, Ognp1);
            var st = _isuExtraS.FindStudent("valera");
            _isuExtraS.AddStudentToOgnp(st, ognpGroup1);
            _isuExtraS.DeleteStudentFromOgnp(_isuExtraS.FindStudent("valera"), ognpGroup1);
            var G = _isuExtraS.FindGroup(st.Group);
            var list = _isuExtraS.GetStudentsNoOgnp(G);
            Assert.Contains(st, list);
        }
        
    }
}