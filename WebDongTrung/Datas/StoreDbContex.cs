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
        public DbSet<BlogMaster>? BlogMasters { get; set; }
        public DbSet<Discount>? Discounts { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CartDetail>().HasKey(p => new { p.IdCart, p.IdProduct });
            modelBuilder.Entity<Warehouse>().HasKey(p=>new{p.Id,p.IdProduct});
            base.OnModelCreating(modelBuilder);
            // Bỏ tiền tố AspNet của các bảng: mặc định
            foreach (var entityType in modelBuilder.Model.GetEntityTypes ()) {
                var tableName = entityType.GetTableName ();
                if (tableName!.StartsWith ("AspNet")) {
                    entityType.SetTableName (tableName.Substring (6));
                }
            }
        }
    }
}
