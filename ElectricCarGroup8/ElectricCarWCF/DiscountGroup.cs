using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace ElectricCarWCF
{
    [DataContract]
    public class DiscountGroup
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public decimal Discount { get; set; }
    }
}
