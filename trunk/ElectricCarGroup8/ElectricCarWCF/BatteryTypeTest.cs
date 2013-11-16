using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace ElectricCarWCF
{
    [DataContract]
    public class BatteryTypeTest
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public int quantity { get; set; }
        [DataMember]
        public double price { get; set; }
        
    }
}
