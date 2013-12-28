using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace ElectricCarWCF
{
    [DataContract]
    public class BookingLine
    {
        public BookingLine()
        {
            station = new Station();
            BatteryType = new BatteryType();
        }
        [DataMember]
        public Station station { get; set; }
        [DataMember]
        public int quantity { get; set; }
        [DataMember]
        public decimal price { get; set; }
        [DataMember]
        public DateTime time { get; set; }
        [DataMember]
        public BatteryType BatteryType { get; set; }
        [DataMember]
        public int bId { get; set; }


    }
}
