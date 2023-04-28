namespace WebDongTrung.DTO.WareHouseDto
{
    public class CreateWareHouseDto
    {
        public DateTime? Import_date { get; set; }
        public List<ProductWareHouses> ProductWarehouses { get; set; } = null!;
        public int? Total_price { get; set; }
    }
    public class ProductWareHouses{
        public int ProductId { get; set; }
        public int ImportQuantity { get; set; }
        public int? ImportPrice { get; set; }
    }
}