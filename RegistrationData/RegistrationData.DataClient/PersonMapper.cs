using RegistrationData.DataAccess;
using RegistrationData.DataClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RegistrationData.DataClient
{
    public class PersonMapper
    {
        public static PersonDAO MapToPersonDAO(Person person)
        {
            var p = new PersonDAO();
            p.Id = person.PersonId;
            p.FirstName = person.FirstName;
            p.LastName = person.LastName;
            p.PersonType = PersonTypeMapper.MapToPersonTypeDAO(person.Type);
            p.Active = person.Active;

            return p;
        }

        public static Person MapToPerson(PersonDAO person)
        {
            var p = new Person();
            p.PersonId = person.Id;
            p.FirstName = person.FirstName;
            p.LastName = person.LastName;
            p.PersonType = PersonTypeMapper.MapToPersonType(person.PersonType).PersonTypeId;
            p.Active = person.Active;

            return p;
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