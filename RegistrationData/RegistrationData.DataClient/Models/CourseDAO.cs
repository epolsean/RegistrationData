using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace RegistrationData.DataClient.Models
{
    [DataContract]
    public class CourseDAO
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public string Department { get; set; }
        [DataMember]
        public PersonDAO Professor { get; set; }
        [DataMember]
        public int StartTime { get; set; }
        [DataMember]
        public int EndTime { get; set; }
        [DataMember]
        public int Capacity { get; set; }
        [DataMember]
        public int Credit { get; set; }
        [DataMember]
        public bool Active { get; set; }
    }
}