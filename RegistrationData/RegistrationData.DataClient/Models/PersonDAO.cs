using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace RegistrationData.DataClient.Models
{
    [DataContract]
    public class PersonDAO
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public PersonTypeDAO PersonType { get; set; }
        [DataMember]
        public bool Active { get; set; }
    }
}