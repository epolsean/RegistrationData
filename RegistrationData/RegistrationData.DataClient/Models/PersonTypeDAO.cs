using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace RegistrationData.DataClient.Models
{
    [DataContract]
    public class PersonTypeDAO
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Type { get; set; }
        [DataMember]
        public bool Active { get; set; }
    }
}