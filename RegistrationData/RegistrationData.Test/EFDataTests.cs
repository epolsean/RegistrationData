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
        public void Test_GetPeople()
        {
            var data = new EFData();
            var actual = data.GetPeople();

            Assert.NotNull(actual);
        }

        [Fact]
        public void Test_GetPersonTypes()
        {
            var data = new EFData();
            var actual = data.GetPersonTypes();

            Assert.NotNull(actual);
        }

        [Fact]
        public void Test_GetSchedules()
        {
            var data = new EFData();
            var actual = data.GetSchedules();

            Assert.NotNull(actual);
        }

        [Fact]
        public void Test_GetCourse()
        {
            var data = new EFData();
            var actual = data.GetCourse(1);

            Assert.NotNull(actual);
        }

        [Fact]
        public void Test_GetPerson()
        {
            var data = new EFData();
            var actual = data.GetPerson(1);

            Assert.NotNull(actual);
        }

        [Fact]
        public void Test_GetPersonType()
        {
            var data = new EFData();
            var actual = data.GetPersonType(1);

            Assert.NotNull(actual);
        }

        [Fact]
        public void Test_GetSchedule()
        {
            var data = new EFData();
            var actual = data.GetSchedule(1, 1);

            Assert.NotNull(actual);
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
        public void Test_AddCourse()
        {
            var data = new EFData();
            Person person = data.GetPerson(5);
            Course course = new Course() { Title = "TST-101", Department ="Quality Control", Professor = person.PersonId, StartTime = 20, EndTime = 21, Capacity = 10, Credit = 5, Active = true};

            var actual = data.AddCourse(course, person);

            if (data.CancelCourse(course))
            {
                Assert.True(actual);
            }
            else
            {
                Assert.True(false);
            }
        }

        [Fact]
        public void Test_CartCourse()
        {
            var data = new EFData();
            Person person = data.GetPerson(2);
            Person professor = data.GetPerson(5);
            Course course = new Course() { Title = "TST-101", Department = "Quality Control", Professor = professor.PersonId, StartTime = 20, EndTime = 21, Capacity = 10, Credit = 5, Active = true };

            if (data.AddCourse(course, professor))
            {
                var actual = data.CartCourse(course, person);

                if (data.CancelCourse(course))
                {
                    Assert.True(actual);
                }
                else
                {
                    Assert.True(false);
                }
            }
            else
            {
                Assert.True(false);
            }
        }

        [Fact]
        public void Test_RegisterCourse()
        {
            var data = new EFData();
            Person person = data.GetPerson(2);
            Person professor = data.GetPerson(5);
            Course course = new Course() { Title = "TST-101", Department = "Quality Control", Professor = professor.PersonId, StartTime = 20, EndTime = 21, Capacity = 10, Credit = 5, Active = true };

            if (data.AddCourse(course, professor))
            {
                if (data.CartCourse(course, person))
                {
                    var actual = data.RegisterCourse(course, person);

                    if (data.CancelCourse(course))
                    {
                        Assert.True(actual);
                    }
                    else
                    {
                        Assert.True(false);
                    }
                }
                else
                {
                    Assert.True(false);
                }
            }
            else
            {
                Assert.True(false);
            }
        }

        [Fact]
        public void Test_DropCourse()
        {
            var data = new EFData();
            Person person = data.GetPerson(2);
            Person professor = data.GetPerson(5);
            Course course = new Course() { Title = "TST-101", Department = "Quality Control", Professor = professor.PersonId, StartTime = 20, EndTime = 21, Capacity = 10, Credit = 5, Active = true };

            if (data.AddCourse(course, professor))
            {
                if (data.CartCourse(course, person))
                {
                    var actual = data.DropCourse(course, person);

                    if (data.CancelCourse(course))
                    {
                        Assert.True(actual);
                    }
                    else
                    {
                        Assert.True(false);
                    }
                }
                else
                {
                    Assert.True(false);
                }
            }
            else
            {
                Assert.True(false);
            }
        }

        [Fact]
        public void Test_CancelCourse()
        {
            var data = new EFData();
            Person professor = data.GetPerson(5);
            Course course = new Course() { Title = "TST-101", Department = "Quality Control", Professor = professor.PersonId, StartTime = 20, EndTime = 21, Capacity = 10, Credit = 5, Active = true };

            if (data.AddCourse(course, professor))
            {
                var actual = data.CancelCourse(course);

                Assert.True(actual);
            }
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
            Person student = new Person() { FirstName = "Test", LastName = "Man", PersonType = 1, Active = true };

            var actual = data.AddPerson(student);

            if (data.RemoveStudent(student))
            {
                Assert.True(actual);
            }
            else
            {
                Assert.True(false);
            }
        }

        [Fact]
        public void Test_RemoveStudent()
        {
            var data = new EFData();
            Person student = new Person() { FirstName = "Test", LastName = "Man", PersonType = 1, Active = true };

            if(data.AddPerson(student))
            {
                var actual = data.RemoveStudent(student);

                Assert.True(actual);
            }
        }
    }
}
