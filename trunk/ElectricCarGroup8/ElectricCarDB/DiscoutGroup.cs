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
    
    public partial class DiscoutGroup
    {
        public DiscoutGroup()
        {
            this.Customer = new HashSet<Customer>();
        }
    
        public int Id { get; set; }
        public string name { get; set; }
        public Nullable<decimal> dgRate { get; set; }
    
        public virtual ICollection<Customer> Customer { get; set; }
    }
}
