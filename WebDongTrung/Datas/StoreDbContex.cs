using Microsoft.EntityFrameworkCore;

namespace WebDongTrung.Datas
{
    public class StoreDbContex: DbContext
    {
        public StoreDbContex(DbContextOptions<StoreDbContex> options) : base(options) { }

        public DbSet<Product>? Products { get; set; }
    }
}
