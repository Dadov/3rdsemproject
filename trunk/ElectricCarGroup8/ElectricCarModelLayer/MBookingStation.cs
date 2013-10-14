using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricCarModelLayer
{
    public class MBookingStation
    {
        public Nullable<DateTime> bookedTime { get; set; }

        public virtual MBooking Booking { get; set; }
        public virtual MStation Station { get; set; }
    }
}
