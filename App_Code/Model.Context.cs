﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

public partial class UK_ExpressEntities : DbContext
{
    public UK_ExpressEntities()
        : base("name=UK_ExpressEntities")
    {
    }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        throw new UnintentionalCodeFirstException();
    }

    public virtual DbSet<PackageDetail> PackageDetails { get; set; }
    public virtual DbSet<Package> Packages { get; set; }
    public virtual DbSet<Recipient> Recipients { get; set; }
    public virtual DbSet<PriceItem> PriceItems { get; set; }
    public virtual DbSet<Service> Services { get; set; }
    public virtual DbSet<PriceList> PriceLists { get; set; }
    public virtual DbSet<Order> Orders { get; set; }
}
