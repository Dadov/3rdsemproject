using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;


namespace ElectricCarWCF
{
    [DataContract]
    public class Booking
    {
        public Booking()
        {
            bookinglines = new List<BookingLine>();
        }

        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int cId { get; set; }
        [DataMember]
        public decimal totalPrice { get; set; }
        [DataMember]
        public string createDate { get; set; }
        [DataMember]
        public string tripStart { get; set; }
        [DataMember]
        public string payStatus { get; set; }
        [DataMember]
        public List<BookingLine> bookinglines { get; set; }
        [DataMember]
        public Customer customer { get; set; }
        [DataMember]
        public int startStationId { get; set; }
    }
}
