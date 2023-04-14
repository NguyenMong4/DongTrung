using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebDongTrung.Datas
{
    public class StoreDbContex : DbContext
    {
        public StoreDbContex(DbContextOptions<StoreDbContex> options) : base(options) { }

        public DbSet<Product>? Products { get; set; }
        public DbSet<Advertisement>? Advertisements { get; set; }
        public DbSet<Blog>? Blogs { get; set; }
        public DbSet<Cart>? Carts { get; set; }
        public DbSet<CartDetail>? CartDetails { get; set; }
        public DbSet<Employee>? Employees { get; set; }
        public DbSet<ProductType>? ProductTypes { get; set; }
        public DbSet<Warehouse>? Warehouses { get; set; }
        public DbSet<ImportBill>? ImportBills { get; set; }
        public DbSet<MasterName>? MasterNames { get; set; }
        public DbSet<Discount>? Discounts { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CartDetail>().HasKey(p => new { p.IdCart, p.IdProduct });
            modelBuilder.Entity<Warehouse>().HasKey(p => new { p.IdBill, p.IdProduct });
            modelBuilder.Entity<MasterName>().HasKey(p => new { p.Code, p.Cd });
            base.OnModelCreating(modelBuilder);
        }
    }
}
