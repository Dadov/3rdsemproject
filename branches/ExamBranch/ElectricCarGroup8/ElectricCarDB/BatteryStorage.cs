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
    
    public partial class BatteryStorage
    {
        public BatteryStorage()
        {
            this.Periods = new HashSet<Period>();
        }
    
        public int Id { get; set; }
        public Nullable<int> btId { get; set; }
        public Nullable<int> sId { get; set; }
        public Nullable<int> storageNumber { get; set; }
    
        public virtual BatteryType BatteryType { get; set; }
        public virtual Station Station { get; set; }
        public virtual ICollection<Period> Periods { get; set; }
    }
}
