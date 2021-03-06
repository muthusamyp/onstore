﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OnStoreModels
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class OnStoreEntities : DbContext
    {
        public OnStoreEntities()
            : base("name=OnStoreEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Image> Images { get; set; }
        public virtual DbSet<ItemCategoryMap> ItemCategoryMaps { get; set; }
        public virtual DbSet<ItemImageMap> ItemImageMaps { get; set; }
        public virtual DbSet<ItemPriceMap> ItemPriceMaps { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<ItemsAvailability> ItemsAvailabilities { get; set; }
        public virtual DbSet<ItemStoreMap> ItemStoreMaps { get; set; }
        public virtual DbSet<ItemStorePriceDiscount> ItemStorePriceDiscounts { get; set; }
        public virtual DbSet<ItemVendorPrice> ItemVendorPrices { get; set; }
        public virtual DbSet<ItemVideoMap> ItemVideoMaps { get; set; }
        public virtual DbSet<PriceDiscount> PriceDiscounts { get; set; }
        public virtual DbSet<Store> Stores { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<UserAddress> UserAddresses { get; set; }
        public virtual DbSet<UserLogin> UserLogins { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Vendor> Vendors { get; set; }
        public virtual DbSet<Video> Videos { get; set; }
        public virtual DbSet<MetricUnit> MetricUnits { get; set; }
        public virtual DbSet<PurchaseRawReceipt> PurchaseRawReceipts { get; set; }
        public virtual DbSet<PurchaseReceipt> PurchaseReceipt { get; set; }
        public virtual DbSet<OrderDeliveryAddressMap> OrderDeliveryAddressMaps { get; set; }
    }
}
