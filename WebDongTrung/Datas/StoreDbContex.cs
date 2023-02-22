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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CartDetail>().HasKey(p => new { p.IdCart, p.IdProduct });
        }
    }
}
