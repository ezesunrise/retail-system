using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RetailSystem.Models;

namespace RetailSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Business> Businesses { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<LocationItem> LocationItems { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleItem> SaleItems { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<PurchaseItem> PurchaseItems { get; set; }
        public DbSet<Transfer> Transfers { get; set; }
        public DbSet<TransferItem> TransferItems { get; set; }
        public DbSet<ReportGroup> ReportGroups { get; set; }
        public DbSet<ReportItem> ReportItems { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Receipt> Receipts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //Item
            builder.Entity<Item>()
                .HasAlternateKey(i => i.Code).HasName("Item_Code");
            builder.Entity<Item>()
                .Property(i => i.UnitCost)
                .HasColumnType("decimal(8,2)");
            builder.Entity<Item>()
                .Property(i => i.UnitPrice)
                .HasColumnType("decimal(8,2)");
            //SaleItem
            builder.Entity<SaleItem>()
                .Property(s => s.UnitPrice)
                .HasColumnType("decimal(8,2)");
            //Sale
            builder.Entity<Sale>()
                .Property(s => s.Total)
                .HasColumnType("decimal(8,2)");
            //LocationItem
            builder.Entity<LocationItem>()
                .Property(i => i.UnitPrice)
                .HasColumnType("decimal(8,2)");
            //Location
            builder.Entity<Location>()
                .Property(l => l.Target)
                .HasColumnType("decimal(8,2)");
            builder.Entity<Location>()
                .HasMany(l => l.OutgoingTransfers)
                .WithOne(t => t.SourceLocation)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Location>()
                .HasMany(l => l.IncomingTransfers)
                .WithOne(t => t.DestinationLocation)
                .OnDelete(DeleteBehavior.Restrict);
            //Purchase
            builder.Entity<Purchase>()
                .HasOne(p => p.Order)
                .WithOne(o => o.Purchase)
                .HasForeignKey<Order>();

            base.OnModelCreating(builder);
        }
    }
}
