using RegistrationData.DataAccess;
using RegistrationData.DataClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RegistrationData.DataClient
{
    public class ScheduleMapper
    {
        public static ScheduleDAO MapToScheduleDAO(Schedule schedule)
        {
            var s = new ScheduleDAO();
            s.Id = schedule.ScheduleId;
            s.Person = PersonMapper.MapToPersonDAO(schedule.Person);
            s.Course = CourseMapper.MapToCourseDAO(schedule.Course);
            s.Active = schedule.Active;

            return s;
        }

        public static Schedule MapToSchedule(ScheduleDAO schedule)
        {
            var s = new Schedule();
            s.ScheduleId = schedule.Id;
            s.PersonId = schedule.Person.Id;
            s.CourseId = schedule.Course.Id;
            s.Active = schedule.Active;

            return s;
        }

        public static object MapTo(object o)
        {
            var properties = 0.GetType().GetProperties();
            var ob = new object();

            foreach (var item in properties.ToList())
            {
                ob.GetType().GetProperty(item.Name).SetValue(ob, item.GetValue(item));
            }

            return ob;
        }
    }
}