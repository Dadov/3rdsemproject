using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace ElectricCarWCF
{
    [DataContract]
    public class RouteStop
    {
        [DataMember]
        public int stationID { get; set; }
        [DataMember]
        public decimal driveHour { get; set; }
        [DataMember]
        public Station station { get; set; }
        [DataMember]
        public decimal distance { get; set; }
        [DataMember]
        public DateTime time { get; set; }
    }
}
