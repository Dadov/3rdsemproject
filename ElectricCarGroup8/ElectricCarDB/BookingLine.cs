//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ElectricCarDB
{
    using System;
    using System.Collections.Generic;
    
    public partial class BookingLine
    {
        public int bId { get; set; }
        public int btId { get; set; }
        public int sId { get; set; }
        public Nullable<int> quantity { get; set; }
        public Nullable<decimal> price { get; set; }
        public Nullable<System.DateTime> time { get; set; }
    
        public virtual BatteryType BatteryType { get; set; }
        public virtual Booking Booking { get; set; }
        public virtual Station Station { get; set; }
    }
}
