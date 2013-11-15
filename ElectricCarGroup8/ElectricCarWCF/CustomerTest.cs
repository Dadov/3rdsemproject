using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace ElectricCarWCF
{
    [DataContract]
    public class CustomerTest
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string name { get; set; }
        
    }
}
