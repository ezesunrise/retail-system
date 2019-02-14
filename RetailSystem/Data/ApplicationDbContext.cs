using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RetailSystem.Models;
using RetailSystem.Services;

namespace RetailSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<AppUser> AppUsers { get; set; }
        //public DbSet<BackOrder> BackOrders { get; set; }
        public DbSet<Business> Businesses { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<LocationItem> LocationItems { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleItem> SaleItems { get; set; }
        public DbSet<PurchaseOrder> Purchases { get; set; }
        public DbSet<Supply> Supplies { get; set; }
        public DbSet<SupplyItem> SupplyItems { get; set; }
        public DbSet<PurchaseOrderItem> PurchaseItems { get; set; }
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
            //AppUser
            AppUserService.CreatePasswordHash("admin-password", out string hash, out string salt);
            var superAdmin = new AppUser
            {
                UserName = "superAdmin",
                Name = "Super Admin",
                Email = "ezesunrise@yahoo.com",
                PhoneNumber = "+2348165859398",
                Role = Role.SuperAdmin,
                PasswordHash = hash,
                PasswordSalt = salt
            };
            var admin = new AppUser
            {
                UserName = "admin",
                Name = "Admin",
                Role = Role.Admin,
                PasswordHash = hash,
                PasswordSalt = salt
            };
            //builder.Entity<AppUser>().HasData(superAdmin, admin);

            builder.Entity<AppUser>()
                .HasOne(u => u.Location)
                .WithMany(l => l.AppUsers)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<AppUser>()
                .HasOne(u => u.Business)
                .WithMany(b => b.AppUsers)
                .OnDelete(DeleteBehavior.Restrict);
            //Category
            builder.Entity<Category>()
                .HasIndex(c => new { c.BusinessId, c.Name }).HasName("Business_Category_Name").IsUnique();
            builder.Entity<Category>()
                .HasIndex(c => new { c.BusinessId, c.Code }).HasName("Business_Category_Code").IsUnique();
            //SubCategory
            builder.Entity<SubCategory>()
                .HasIndex(s => new { s.Name, s.CategoryId }).HasName("SubCategory_Name_CategoryId").IsUnique();
            //Item
            builder.Entity<Item>()
                .HasOne(i => i.Category)
                .WithMany(c => c.Items)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Item>()
                .HasAlternateKey(i => new { i.CategoryId, i.Code }).HasName("Category_Item_Code");
            builder.Entity<Item>()
                .HasIndex(i => new { i.CategoryId, i.Description }).HasName("Category_Item_Description");
            builder.Entity<Item>()
                .Property(i => i.UnitCost)
                .HasColumnType("decimal(10,2)");
            builder.Entity<Item>()
                .Property(i => i.UnitPrice)
                .HasColumnType("decimal(10,2)");
            //SaleItem
            builder.Entity<SaleItem>()
                .HasKey(l => new { l.SaleId, l.ItemId })
                .HasName("SaleItem_Id");
            builder.Entity<SaleItem>()
                .Property(s => s.UnitPrice)
                .HasColumnType("decimal(10,2)");
            //Sale
            builder.Entity<Sale>()
                .HasIndex(s => new { s.LocationId, s.ReferenceNumber }).HasName("Location_Sale_ReferenceNumber").IsUnique();
            //Supply
            builder.Entity<Supply>()
                .HasIndex(s => new { s.LocationId, s.ReferenceNumber }).HasName("Location_Supply_ReferenceNumber").IsUnique();
            //LocationItem
            builder.Entity<LocationItem>()
                .HasKey(l => new { l.LocationId, l.ItemId })
                .HasName("LocationItem_Id");
            builder.Entity<LocationItem>()
                .Property(i => i.UnitPrice)
                .HasColumnType("decimal(10,2)");
            //Location
            builder.Entity<Location>()
                .HasAlternateKey(l => l.Code).HasName("Location_Code");
            builder.Entity<Location>()
                .HasIndex(l => new { l.BusinessId, l.Name }).HasName("Business_Location_Name").IsUnique();
            builder.Entity<Location>()
                .Property(l => l.Target)
                .HasColumnType("decimal(12,2)");
            builder.Entity<Location>()
                .HasMany(l => l.OutgoingTransfers)
                .WithOne(t => t.SourceLocation)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Location>()
                .HasMany(l => l.IncomingTransfers)
                .WithOne(t => t.DestinationLocation)
                .OnDelete(DeleteBehavior.Restrict);
            //PurchaseOrderItem
            builder.Entity<PurchaseOrderItem>()
                .HasKey(l => new { l.PurchaseOrderId, l.ItemId })
                .HasName("PurchaseOrderItem_Id");
            builder.Entity<PurchaseOrderItem>()
                .Property(p => p.UnitCost)
                .HasColumnType("decimal(10,2)");
            //TransferItem
            builder.Entity<TransferItem>()
                .HasKey(l => new { l.TransferId, l.ItemId })
                .HasName("TransferItem_Id");
            //InvoiceItem
            builder.Entity<InvoiceItem>()
                .HasKey(l => new { l.InvoiceId, l.ItemId })
                .HasName("InvoiceItem_Id");
            //SupplyItem
            builder.Entity<SupplyItem>()
                .HasKey(s => new { s.SupplyId, s.ItemId })
                .HasName("SupplyItem_Id");
            builder.Entity<SupplyItem>()
                .Property(s => s.UnitPrice)
                .HasColumnType("decimal(10,2)");
            //ReportItem
            builder.Entity<ReportItem>()
                .HasKey(l => new { l.ReportGroupId, l.ItemId })
                .HasName("ReportItem_Id");
            //Supplier
            builder.Entity<Supplier>()
                .HasIndex(s => new { s.BusinessId, s.SupplierNumber }).HasName("Business_Supplier_Number").IsUnique();
            builder.Entity<Supplier>()
                .HasIndex(s => new { s.BusinessId, s.Name }).HasName("Business_Supplier_Name").IsUnique();
            //Manufacturer
            builder.Entity<Manufacturer>()
                .HasIndex(m => new { m.BusinessId, m.Name }).HasName("Business_Manufacturer_Name").IsUnique();
            //PurchaseOrder
            builder.Entity<PurchaseOrder>()
                .HasIndex(p => new { p.LocationId, p.OrderNumber }).HasName("Business_Order_Number").IsUnique();
            //Transfer
            builder.Entity<Transfer>()
                .HasIndex(t => new { t.SourceLocationId, t.DestinationLocationId, t.TransferNumber }).HasName("Location_Transfer_Number").IsUnique();
            //Invoice
            builder.Entity<Invoice>()
                .HasIndex(i => i.InvoiceNumber).HasName("Invoice_Number").IsUnique();
            //Receipt
            builder.Entity<Receipt>()
                .HasIndex(r => r.ReceiptNumber).HasName("Receipt_Number").IsUnique();
            builder.Entity<Receipt>()
                .Property(r => r.CashPaid)
                .HasColumnType("decimal(10,2)");
            //Unit
            builder.Entity<Unit>()
                .HasIndex(u => u.Name)
                .HasName("Unit_Name");

            base.OnModelCreating(builder);
        }
    }
}
