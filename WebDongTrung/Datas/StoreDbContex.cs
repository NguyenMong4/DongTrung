using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebDongTrung.Datas
{
    public class StoreDbContex : DbContext
    {
        public StoreDbContex(DbContextOptions<StoreDbContex> options) : base(options) {
           
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           // var connectinString = "Server=222.252.22.153;Port = 11006;Database=DongTrungHaThao;user id=root;password=root";
             var connectinString = "Server=14.255.204.144;Port = 3306;Database=DongTrungHaThao;user id=root;password=nguyena4";
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseMySql(connectinString, ServerVersion.AutoDetect(connectinString));
        }
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
            modelBuilder.Entity<Warehouse>().HasKey(p => new { p.BillId, p.ProductId });
            modelBuilder.Entity<MasterName>().HasKey(p => new { p.Code, p.Cd });
            base.OnModelCreating(modelBuilder);
        }
    }
}
