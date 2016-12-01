using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using RegistrationData.DataClient.Models;
using RegistrationData.DataAccess;

namespace RegistrationData.DataClient
{
    public class RegistrationService : IRegistrationService
    {
        private EFData db = new EFData();

        public bool AddCourse(CourseDAO course)
        {
            var c = CourseMapper.MapToCourse(course);
            var p = db.GetPerson(course.Professor.Id);

            return db.AddCourse(c, p);
        }

        public bool AddPerson(PersonDAO person)
        {
            var p = PersonMapper.MapToPerson(person);

            return db.AddPerson(p);
        }

        public bool AddPersonType(PersonTypeDAO personType)
        {
            var pt = PersonTypeMapper.MapToPersonType(personType);

            return db.AddPersonType(pt);
        }

        public bool CancelCourse(CourseDAO course)
        {
            var c = CourseMapper.MapToCourse(course);

            return db.CancelCourse(c);
        }

        public bool CartCourse(CourseDAO course, PersonDAO person)
        {
            var c = CourseMapper.MapToCourse(course);
            var p = PersonMapper.MapToPerson(person);

            return db.CartCourse(c, p);
        }

        public bool DropCourse(CourseDAO course, PersonDAO person)
        {
            var c = CourseMapper.MapToCourse(course);
            var p = PersonMapper.MapToPerson(person);

            return db.DropCourse(c, p);
        }

        public List<PersonDAO> GetAllActiveStudents()
        {
            var p = new List<PersonDAO>();

            foreach (var person in db.GetAllActiveStudents())
            {
                p.Add(PersonMapper.MapToPersonDAO(person));
            }

            return p;
        }

        public List<CourseDAO> GetAllFullCourses()
        {
            var c = new List<CourseDAO>();

            foreach (var course in db.GetAllFullCourses())
            {
                c.Add(CourseMapper.MapToCourseDAO(course));
            }

            return c;
        }

        public List<CourseDAO> GetAllOpenCourses()
        {
            var c = new List<CourseDAO>();

            foreach (var course in db.GetAllOpenCourses())
            {
                c.Add(CourseMapper.MapToCourseDAO(course));
            }

            return c;
        }

        public CourseDAO GetCourse(int cid)
        {
            var course = db.GetCourse(cid);
            var c = CourseMapper.MapToCourseDAO(course);

            return c;
        }

        public List<CourseDAO> GetCourses()
        {
            var c = new List<CourseDAO>();

            foreach (var course in db.GetCourses())
            {
                c.Add(CourseMapper.MapToCourseDAO(course));
            }

            return c;
        }

        public List<PersonDAO> GetEnrolledStudentsByCourse(CourseDAO course)
        {
            var p = new List<PersonDAO>();

            foreach (var person in db.GetEnrolledStudentsByCourse(CourseMapper.MapToCourse(course)))
            {
                p.Add(PersonMapper.MapToPersonDAO(person));
            }

            return p;
        }

        public List<PersonDAO> GetPeople()
        {
            var p = new List<PersonDAO>();

            foreach (var person in db.GetPeople())
            {
                p.Add(PersonMapper.MapToPersonDAO(person));
            }

            return p;
        }

        public PersonDAO GetPerson(int pid)
        {
            var person = db.GetPerson(pid);
            var p = PersonMapper.MapToPersonDAO(person);

            return p;
        }

        public PersonTypeDAO GetPersonType(int pid)
        {
            var personType = db.GetPersonType(pid);
            var pt = PersonTypeMapper.MapToPersonTypeDAO(personType);

            return pt;
        }

        public List<PersonTypeDAO> GetPersonTypes()
        {
            var pt = new List<PersonTypeDAO>();

            foreach (var personType in db.GetPersonTypes())
            {
                pt.Add(PersonTypeMapper.MapToPersonTypeDAO(personType));
            }

            return pt;
        }

        public ScheduleDAO GetSchedule(int cid, int pid)
        {
            var schedule = db.GetSchedule(cid, pid);
            var s = ScheduleMapper.MapToScheduleDAO(schedule);

            return s;
        }

        public List<ScheduleDAO> GetSchedules()
        {
            var s = new List<ScheduleDAO>();

            foreach (var schedule in db.GetSchedules())
            {
                s.Add(ScheduleMapper.MapToScheduleDAO(schedule));
            }

            return s;
        }

        public List<CourseDAO> GetStudentSchedule(PersonDAO person)
        {
            var c = new List<CourseDAO>();

            foreach (var course in db.GetStudentSchedule(PersonMapper.MapToPerson(person)))
            {
                c.Add(CourseMapper.MapToCourseDAO(course));
            }

            return c;
        }

        public bool ModifyCourse(CourseDAO course, int newCapacity, int newStart, int newEnd)
        {
            var c = CourseMapper.MapToCourse(course);

            return db.ModifyCourse(c, newCapacity, newStart, newEnd);
        }

        public bool RegisterCourse(CourseDAO course, PersonDAO person)
        {
            var c = CourseMapper.MapToCourse(course);
            var p = PersonMapper.MapToPerson(person);

            return db.RegisterCourse(c, p);
        }

        public bool RemoveStudent(PersonDAO student)
        {
            var p = PersonMapper.MapToPerson(student);

            return db.RemoveStudent(p);
        }
    }
}
