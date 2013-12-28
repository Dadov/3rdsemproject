using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace ElectricCarWCF
{
    [DataContract]
    public class Employee
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public string FName { get; set; }

        [DataMember]
        public string LName { get; set; }

        [DataMember]
        public string Address { get; set; }

        [DataMember]
        public string Country { get; set; }

        [DataMember]
        public string Phone { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public List<LogInfo> LogInfos { get; set; }

        [DataMember]
        public int StationID { get; set; }

        [DataMember]
        // string as a placeholder, for EmployeePosition enum
        public string Position { get; set; }
    }
}
