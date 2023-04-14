using WebDongTrung.Datas;

namespace WebDongTrung.Models
{
    public class WarehouseModel : SystemProperties
    {
        public string Id { get; set; } = null!;
        public DateTime? Import_date { get; set; }
        public List<ProductWarehouseModel> ProductWarehouses { get; set; } = null!;
        public int? TotalPrice { get; set; }
    }
}