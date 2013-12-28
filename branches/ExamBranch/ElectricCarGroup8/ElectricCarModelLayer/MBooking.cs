using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricCarModelLayer
{
    public class MBooking
    {
        public MBooking()
        {
            bookinglines = new List<MBookingLine>();
        }

        
        public int Id { get; set; }
        public Nullable<int> cId { get; set; }
        public Nullable<decimal> totalPrice { get; set; }
        public Nullable<System.DateTime> createDate { get; set; }
        public Nullable<System.DateTime> tripStart { get; set; }
        public string creaditCard { get; set; }
        public List<MBookingLine> bookinglines { get; set; }
        public MCustomer customer {get; set;}
    }
}
