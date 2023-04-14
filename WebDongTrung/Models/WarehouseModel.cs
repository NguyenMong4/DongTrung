namespace WebDongTrung.Models
{
    public class WarehouseModel
    {
        public List<ProductWarehouse> ProductWarehouses { get; set; } = null!;
    }
    public class ProductWarehouse
    {
        public int IdProduct { get; set; }
        public int ImportQuantity { get; set; }
        public int? ImportPrice { get; set; }
        public int? RealityQuantity { get; set; }
        public int? SystemQuantity { get; set; }
    }
}