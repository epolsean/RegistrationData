using RegistrationData.DataAccess;
using RegistrationData.DataClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RegistrationData.DataClient
{
    public class CourseMapper
    {
        public static CourseDAO MapToCourseDAO(Course course)
        {
            var c = new CourseDAO();
            c.Id = course.CourseId;
            c.Title = course.Title;
            c.Department = course.Department;
            c.Professor = PersonMapper.MapToPersonDAO(course.Person);
            c.StartTime = course.StartTime;
            c.EndTime = course.EndTime;
            c.Capacity = course.Capacity;
            c.Credit = course.Credit;
            c.Active = course.Active;

            return c;
        }

        public static Course MapToCourse(CourseDAO course)
        {
            var c = new Course();
            c.CourseId = course.Id;
            c.Title = course.Title;
            c.Department = course.Department;
            c.Professor = PersonMapper.MapToPerson(course.Professor).PersonId;
            c.StartTime = course.StartTime;
            c.EndTime = course.EndTime;
            c.Capacity = course.Capacity;
            c.Credit = course.Credit;
            c.Active = course.Active;

            return c;
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