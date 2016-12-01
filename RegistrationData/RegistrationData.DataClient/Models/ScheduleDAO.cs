using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace RegistrationData.DataClient.Models
{
    [DataContract]
    public class ScheduleDAO
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public PersonDAO Person { get; set; }
        [DataMember]
        public CourseDAO Course { get; set; }
        [DataMember]
        public bool Active { get; set; }
    }
}