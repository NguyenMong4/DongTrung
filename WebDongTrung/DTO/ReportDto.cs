namespace WebDongTrung.DTO
{
    public class ReportDto
    {
        public int SuccessOrder { get; set; }
        public int RefunOrder { get; set; }
        public int Total { get; set; }
        public List<ProductsOrder>? ProductsOrderLst { get; set; }
    }

    public class ProductsOrder
    {
        public int Idproduct { get; set; }
        public int Quantity { get; set; }
        public string? ProductName { get; set; }
        public string? ProductPhoto { get; set; }
    }
}