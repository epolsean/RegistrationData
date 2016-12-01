using RegistrationData.DataAccess;
using RegistrationData.DataClient;
using RegistrationData.DataClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RegistrationData.Test
{
    public class EFDataTests
    {
        [Fact]
        public void Test_GetCourses()
        {
            var data = new EFData();
            var actual = data.GetCourses();

            Assert.NotNull(actual);
        }

        [Fact]
        public void Test_GetCourseEndTime()
        {
            var data = new EFData();
            var expected = 0;

            var actual = data.GetCourse(3).EndTime;

            Assert.NotEqual(actual, expected);
        }

        [Fact]
        public void Test_GetAllFullCourses()
        {
            var data = new EFData();
            var expected = 0;

            var actual = data.GetAllFullCourses();

            Assert.Equal(actual.Count(), expected);
        }

        [Fact]
        public void Test_GetAllOpenCourses()
        {
            var data = new EFData();
            var actual = data.GetAllOpenCourses();

            Assert.NotEqual(actual.Count(), 0);
        }

        [Fact]
        public void Test_GetEnrolledStudentByCourse()
        {
            var data = new EFData();
            Course testCourse = new Course() { CourseId = 1 };
            var actual = data.GetEnrolledStudentsByCourse(testCourse);

            Assert.NotEqual(actual.Count(), 0);
        }

        [Fact]
        public void Test_GetAllActiveStudents()
        {
            var data = new EFData();
            var actual = data.GetAllActiveStudents();

            Assert.NotNull(actual);
        }

        [Fact]
        public void Test_GetStudentSchedule()
        {
            var data = new EFData();
            Person person = new Person() { PersonId = 1 };
            var actual = data.GetStudentSchedule(person);

            Assert.NotEqual(actual.Count(), 0);
        }

        [Fact]
        public void Test_AddCourse()
        {
            var data = new EFData();
            Person person = data.GetPerson(5);
            Course course = new Course() { Title = "TST-101", Department ="Quality Control", Professor = person.PersonId, StartTime = 5, EndTime = 6, Capacity = 10, Credit = 5, Active = true};
            var actual = data.AddCourse(course, person);

            Assert.True(actual);
        }

        [Fact]
        public void Test_AddCourse2()
        {
            var data = new RegistrationService();
            var person = data.GetPerson(5);
            var course = new CourseDAO() { Title = "TST-101", Department = "Quality Control", Professor = person, StartTime = 5, EndTime = 6, Capacity = 10, Credit = 5, Active = true };
            var actual = data.AddCourse(course);

            Assert.True(actual);
        }

        [Fact]
        public void Test_CartCourse()
        {
            var data = new EFData();
            Person person = data.GetPerson(2);
            Course course = data.GetCourse(5);
            var actual = data.CartCourse(course, person);

            Assert.True(actual);
        }

        [Fact]
        public void Test_RegisterCourse()
        {
            var data = new EFData();
            Person person = data.GetPerson(2);
            Course course = data.GetCourse(5);
            var actual = data.RegisterCourse(course, person);

            Assert.True(actual);
        }

        [Fact]
        public void Test_DropCourse()
        {
            var data = new EFData();
            Person person = data.GetPerson(2);
            Course course = data.GetCourse(5);
            var actual = data.DropCourse(course, person);

            Assert.True(actual);
        }

        [Fact]
        public void Test_CancelCourse()
        {
            var data = new EFData();
            var course = data.GetCourse(5);

            var actual = data.CancelCourse(course);

            Assert.True(actual);
        }

        [Fact]
        public void Test_CancelCourse2()
        {
            var data = new RegistrationService();
            var course = data.GetCourse(4);

            var actual = data.CancelCourse(course);

            Assert.True(actual);
        }

        [Fact]
        public void Test_ModifyCourse()
        {
            var data = new EFData();
            Course course = data.GetCourse(2);
            var actual = data.ModifyCourse(course, 35, 0, 0);

            Assert.True(actual);
        }

        [Fact]
        public void Test_AddStudent()
        {
            var data = new EFData();
            Person newStudent = new Person() { FirstName = "Kevin", LastName = "Hart", PersonType = 1, Active = true };
            var actual = data.AddPerson(newStudent);

            Assert.True(actual);
        }

        [Fact]
        public void Test_RemoveStudent()
        {
            var data = new EFData();
            Person removeStudent = data.GetPerson(8);
            var actual = data.RemoveStudent(removeStudent);

            Assert.True(actual);
        }
    }
}
