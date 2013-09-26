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
    
    public partial class BatteryType
    {
        public BatteryType()
        {
            this.Battery = new HashSet<Battery>();
            this.BatteryStorage = new HashSet<BatteryStorage>();
            this.BookingLine = new HashSet<BookingLine>();
        }
    
        public int Id { get; set; }
        public string name { get; set; }
        public string producer { get; set; }
        public Nullable<decimal> capacity { get; set; }
        public Nullable<decimal> exchangeCost { get; set; }
        public Nullable<int> storageNumber { get; set; }
    
        public virtual ICollection<Battery> Battery { get; set; }
        public virtual ICollection<BatteryStorage> BatteryStorage { get; set; }
        public virtual ICollection<BookingLine> BookingLine { get; set; }
    }
}
