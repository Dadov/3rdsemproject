using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace ElectricCarWCF
{
    [DataContract]
    public class LogInfo
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public string LoginName { get; set; }

        [DataMember]
        public string Password { get; set; }
    }
}
