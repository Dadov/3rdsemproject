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
    
    public partial class Connection
    {
        public int sId1 { get; set; }
        public int sId2 { get; set; }
        public Nullable<decimal> distance { get; set; }
        public Nullable<decimal> driveHour { get; set; }
    
        public virtual Station Station { get; set; }
        public virtual Station Station1 { get; set; }
    }
}
