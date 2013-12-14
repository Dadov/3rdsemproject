using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ElectricCarWCF
{
    [DataContract]
    public class BatteryType
    {
        [DataMember]
        public int ID;

        [DataMember]
        public String name;

        [DataMember]
        public String producer;

        [DataMember]
        public decimal capacity;

        [DataMember]
        public decimal exchangeCost;

        [DataMember]
        public int quantity { get; set; }
        [DataMember]
        public double price { get; set; }


    }
}
