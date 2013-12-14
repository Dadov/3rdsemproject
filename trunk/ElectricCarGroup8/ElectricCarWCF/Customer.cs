using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace ElectricCarWCF
{
    [DataContract]
    public class Customer
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
        public string PaymentStatus { get; set; }

        [DataMember]
        public DiscountGroup DiscountGroup { get; set; }

        [DataMember]
        public string name { get; set; }
    }
}
