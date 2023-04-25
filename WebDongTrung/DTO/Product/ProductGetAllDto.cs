namespace WebDongTrung.DTO.Product
{
    public class ProductGetAllDto
    {
        public int? MaxPage { get; set; }
        public int? TotalProduct { get; set; }
        public List<Datas.Product>? Products { get; set; }
    }
}