﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ElectricCarEntities : DbContext, IDisposable
    {
        public ElectricCarEntities()
            : base("name=ElectricCarEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Battery> Battery { get; set; }
        public DbSet<BatteryStorage> BatteryStorage { get; set; }
        public DbSet<BatteryType> BatteryType { get; set; }
        public DbSet<Booking> Booking { get; set; }
        public DbSet<BookingLine> BookingLine { get; set; }
        public DbSet<Connection> Connection { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<DiscoutGroup> DiscoutGroup { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<LoginInfo> LoginInfo { get; set; }
        public DbSet<Period> Period { get; set; }
        public DbSet<Person> Person { get; set; }
        public DbSet<Station> Station { get; set; }
    }
}
