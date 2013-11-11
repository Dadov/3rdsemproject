using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace ElectricCarWCF
{
    [DataContract]
    public class Station
    {
        [DataMember]
        public string Id;
        [DataMember]
        public string Name;
        [DataMember]
        public string Address;
        [DataMember]
        public string Country;
        [DataMember]
        public string State;
    }
}
