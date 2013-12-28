using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ElectricCarWCF
{
    [DataContract]
    public class Period
    {
        [DataMember]
        public int bsID;

        [DataMember]
        public int availNumber;

        [DataMember]
        public int bookedNumber;

        [DataMember]
        public DateTime time;
    }
}
