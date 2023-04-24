using WebDongTrung.Datas;

namespace WebDongTrung.Models
{
    public class ImportBillModel
    {
        public string? Id { get; set; }
        public DateTime? Import_date { get; set; }
        public List<ProductWarehouseModel> ProductWarehouses { get; set; } = null!;
        public int? Total_price { get; set; }
    }
}