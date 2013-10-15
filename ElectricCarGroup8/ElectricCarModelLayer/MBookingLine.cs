﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectricCarModelLayer
{
    public class MBookingLine
    {
        
        public Nullable<int> quantity { get; set; }
        public Nullable<decimal> price { get; set; }
        public Nullable<DateTime> time { get; set; }

        public MBatteryType BatteryType { get; set; }
        public MBooking Booking { get; set; }
        public MStation Station { get; set; }
    }
}
