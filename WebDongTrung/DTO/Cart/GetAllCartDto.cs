namespace WebDongTrung.DTO.Cart
{
    public class GetAllCartDto
    {
        public int? MaxPage { get; set; }
        public List<Datas.Cart>? Carts { get; set; }
    }
}