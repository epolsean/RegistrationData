using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrationData.DataAccess
{
    public class EFData
    {
        
        private RegistrationDBEntities db = new RegistrationDBEntities();

        #region Get Functions
        public List<Person> GetPeople()
        {
            return db.People.ToList();
        }

        public List<Course> GetCourses()
        {
            return db.Courses.ToList();
        }

        public List<Schedule> GetSchedules()
        {
            return db.Schedules.ToList();
        }

        public List<PersonType> GetPersonTypes()
        {
            return db.PersonTypes.ToList();
        }
        #endregion

        #region Get Search Functions
        public List<Course> GetAllFullCourses()
        {
            var fullCourses = db.Courses.Where(c => c.Capacity == db.Schedules.Where(s => s.CourseId == c.CourseId).Count());

            return fullCourses.ToList();
        }

        public Person GetPerson(int pid)
        {
            return db.People.Where(p => p.PersonId == pid).Single();
        }

        public Course GetCourse(int cid)
        {
            return db.Courses.Where(c => c.CourseId == cid).Single();
        }

        public Schedule GetSchedule(int cid, int pid)
        {
            return db.Schedules.Where(s => s.CourseId == cid && s.PersonId == pid).Single();
        }

        public PersonType GetPersonType(int pid)
        {
            return db.PersonTypes.Where(pt => pt.PersonTypeId == pid).Single();
        }

        public List<Course> GetAllOpenCourses()
        {
            var openCourses = db.Courses.Where(c => c.Capacity > db.Schedules.Where(s => s.CourseId == c.CourseId).Count());

            return openCourses.ToList();
        }

        public List<Person> GetEnrolledStudentsByCourse(Course course)
        {
            var scheduleInfo = db.Schedules.Where(s => s.CourseId == course.CourseId).ToList();
            var enrolledStudents = db.People.Join(db.Schedules,
                s => s.PersonId,
                p => p.PersonId,
                (p, s) => new{
                    person = p,
                    schedules = s
                })
                .Where(sp => sp.schedules.CourseId == course.CourseId && sp.schedules.Active == true && sp.person.PersonType == 1)
                .Select(x => x.person);

            return enrolledStudents.ToList();
        }

        public List<Person> GetAllActiveStudents()
        {
            var activeStudents = db.People.Where(p => p.Active && p.PersonType == 1);

            return activeStudents.ToList();
        }

        public List<Course> GetStudentSchedule(Person person)
        {
            var schedule = db.Courses.Join(db.Schedules,
                s => s.CourseId,
                c => c.CourseId,
                (c, s) => new {
                    course = c,
                    schedules = s
                })
                .Where(cs => cs.schedules.PersonId == person.PersonId)
                .Select(x => x.course);

            return schedule.ToList();
        }
        #endregion


        public bool AddPerson(Person person)
        {
            db.People.Add(person);

            return db.SaveChanges() > 0;
        }

        public bool AddPersonType(PersonType personType)
        {
            db.PersonTypes.Add(personType);

            return db.SaveChanges() > 0;
        }

        public bool AddCourse(Course course, Person person)
        {
            if (course.Professor == person.PersonId && CheckTimeAvailability(course, person) == true)
            {
                db.Courses.Add(course);

                if(db.SaveChanges() > 0)
                {
                    if(AddSchedule(course, person))
                    {

                    }
                }
            }

            return db.SaveChanges() > 0;
        }

        public bool AddSchedule(Course course, Person person)
        {
            var schedule = new Schedule() { Course = course, Person = person, Active = true };

            db.Schedules.Add(schedule);

            return db.SaveChanges() > 0;
        }

        public bool RegisterCourse(Course course, Person person)
        {

            if (db.Schedules.Where(s => s.PersonId == person.PersonId && s.CourseId == course.CourseId && !s.Active).Count() != 0)
            {
                Schedule updateClass = GetSchedule(course.CourseId, person.PersonId);
                updateClass.Active = true;
                var entry = db.Entry<Schedule>(updateClass);

                entry.State = EntityState.Modified;
            }

            return db.SaveChanges() > 0;
        }

        public bool CartCourse(Course course, Person person)
        {
            if (db.Schedules.Where(s => s.PersonId == person.PersonId && s.CourseId == course.CourseId).Count() == 0)
            {
                Schedule newClass = new Schedule() { PersonId = person.PersonId, CourseId = course.CourseId, Active = false };
                db.Schedules.Add(newClass);
            }

            return db.SaveChanges() > 0;
        }

        public bool DropCourse(Course course, Person person)
        {
            if (db.Schedules.Where(s => s.PersonId == person.PersonId && s.CourseId == course.CourseId).Count() != 0)
            {
                Schedule dropClass = GetSchedule(course.CourseId, person.PersonId);
                var entry = db.Entry<Schedule>(dropClass);

                entry.State = EntityState.Deleted;
            }

            return db.SaveChanges() > 0;
        }

        public bool CancelCourse(Course course)
        {
            if (db.Courses.Where(c => c.CourseId == course.CourseId).Count() != 0)
            {
                var allPeople = new List<Person>();

                foreach (var item in GetSchedules())
                {
                    if (item.CourseId == course.CourseId)
                    {
                        if (DropCourse(course, item.Person))
                        {
                            continue;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }

                var entry = db.Entry<Course>(course);

                entry.State = EntityState.Deleted;
            }

            return db.SaveChanges() > 0;
        }

        public bool ModifyCourse(Course course, int newCapacity, int newStart, int newEnd)
        {
            int oldStart = course.StartTime;
            int oldEnd = course.EndTime;
            if (db.Courses.Where(c => c.CourseId == course.CourseId).Count() != 0)
            {
                if(newCapacity != 0)
                {
                    course.Capacity = newCapacity;
                }
                if(newStart != 0)
                {
                    course.StartTime = newStart;
                }
                if(newEnd != 0)
                {
                    course.EndTime = newEnd;
                }

                if(newStart != 0 || newEnd != 0)
                {
                    if(CheckTimeAvailability(course, GetPerson(course.Professor)))
                    {
                        var entry = db.Entry<Course>(course);

                        entry.State = EntityState.Modified;
                    }
                    else if(!CheckTimeAvailability(course, GetPerson(course.Professor)) && (newStart == oldStart || newEnd == oldEnd))
                    {
                        var entry = db.Entry<Course>(course);

                        entry.State = EntityState.Modified;
                    }
                    else
                    {
                        course.StartTime = oldStart;
                        course.EndTime = oldEnd;

                        return true;
                    }
                }
                else
                {
                    var entry = db.Entry<Course>(course);

                    entry.State = EntityState.Modified;
                }
            }

            return db.SaveChanges() > 0;
        }

        public bool RemoveStudent(Person student)
        {
            if(db.People.Where(p => p.PersonId == student.PersonId).Count() != 0)
            {
                var enrolledCourses = GetStudentSchedule(student);

                foreach (Course course in enrolledCourses)
                {
                    DropCourse(course, student);
                }

                var entry = db.Entry<Person>(student);

                entry.State = EntityState.Deleted;
            }

            return db.SaveChanges() > 0;
        }

        private bool CheckTimeAvailability(Course course, Person person)
        {
            if (person.PersonType == 1 )
            {
                var studentCourses = GetStudentSchedule(person);
                foreach (Course sc in studentCourses)
                {
                    if ((sc.StartTime == course.StartTime ||
                        sc.EndTime == course.EndTime ||
                        course.StartTime > sc.StartTime && course.EndTime < sc.EndTime ||
                        course.StartTime < sc.StartTime && course.EndTime > sc.EndTime ||
                        course.StartTime < sc.StartTime && course.EndTime > sc.StartTime && course.EndTime < sc.EndTime ||
                        course.StartTime > sc.StartTime && course.StartTime < sc.EndTime && course.EndTime > sc.EndTime) != true)
                    {
                        return false;
                    }
                }
                return true;
            }
            else if (person.PersonType == 2 )
            {
                if (db.Courses.Where(c => c.Professor == course.Professor &&
                                    (c.StartTime == course.StartTime ||
                                     c.EndTime == course.EndTime ||
                                     course.StartTime > c.StartTime && course.EndTime < c.EndTime ||
                                     course.StartTime < c.StartTime && course.EndTime > c.EndTime ||
                                     course.StartTime < c.StartTime && course.EndTime > c.StartTime && course.EndTime < c.EndTime ||
                                     course.StartTime > c.StartTime && course.StartTime < c.EndTime && course.EndTime > c.EndTime)).Count() == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
