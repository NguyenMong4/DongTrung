
namespace WebDongTrung.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal? Price { get; set; }
        public int? Discount { get; set; }
        public string? Photo { get; set; }
        public int? ProductTypeId { get; set; }
        public int? RealityQuantity {get;set;}
        public int? SystemQuantity {get;set;}
    }
}