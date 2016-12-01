using RegistrationData.DataAccess;
using RegistrationData.DataClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RegistrationData.DataClient
{
    public class PersonTypeMapper
    {
        public static PersonTypeDAO MapToPersonTypeDAO(PersonType personType)
        {
            var pt = new PersonTypeDAO();
            pt.Id = personType.PersonTypeId;
            pt.Type = personType.Type;
            pt.Active = personType.Active;

            return pt;
        }

        public static PersonType MapToPersonType(PersonTypeDAO personType)
        {
            var pt = new PersonType();
            pt.PersonTypeId = personType.Id;
            pt.Type = personType.Type;
            pt.Active = personType.Active;

            return pt;
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