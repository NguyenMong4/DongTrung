namespace WebDongTrung.DTO.Cart
{
    public class CartCreateDto
    {
        public int TotalPrice { get; set; }
        public int Status { get; set; }
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string? Note { get; set; }
        public int Payment { get; set; }
        public string PersonName { get; set; } = null!;
        public DateTime ReceivedDate { get; set; }
        public List<CartDetails> CartDetailModel { get; set; } = null!;
    }
    public class CartDetails
    {
        public int IdProduct { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public string? Name { get; set; }
    }
}