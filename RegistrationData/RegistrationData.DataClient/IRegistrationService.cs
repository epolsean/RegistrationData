using RegistrationData.DataClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace RegistrationData.DataClient
{
    [ServiceContract]
    public interface IRegistrationService
    {
        [OperationContract]
        List<CourseDAO> GetCourses();

        [OperationContract]
        List<PersonDAO> GetPeople();

        [OperationContract]
        List<PersonTypeDAO> GetPersonTypes();

        [OperationContract]
        List<ScheduleDAO> GetSchedules();

        [OperationContract]
        CourseDAO GetCourse(int cid);

        [OperationContract]
        PersonDAO GetPerson(int pid);

        [OperationContract]
        PersonTypeDAO GetPersonType(int pid);

        [OperationContract]
        ScheduleDAO GetSchedule(int cid, int pid);

        [OperationContract]
        List<CourseDAO> GetAllFullCourses();

        [OperationContract]
        List<CourseDAO> GetAllOpenCourses();

        [OperationContract]
        List<PersonDAO> GetEnrolledStudentsByCourse(CourseDAO course);

        [OperationContract]
        List<PersonDAO> GetAllActiveStudents();

        [OperationContract]
        List<CourseDAO> GetStudentSchedule(PersonDAO person);

        [OperationContract]
        bool AddCourse(CourseDAO course);

        [OperationContract]
        bool AddPerson(PersonDAO person);

        [OperationContract]
        bool AddPersonType(PersonTypeDAO personType);

        [OperationContract]
        bool RegisterCourse(CourseDAO course, PersonDAO person);

        [OperationContract]
        bool CartCourse(CourseDAO course, PersonDAO person);

        [OperationContract]
        bool DropCourse(CourseDAO course, PersonDAO person);

        [OperationContract]
        bool CancelCourse(CourseDAO course);

        [OperationContract]
        bool ModifyCourse(CourseDAO course, int newCapacity, int newStart, int newEnd);

        [OperationContract]
        bool RemoveStudent(PersonDAO student);

    }
}
