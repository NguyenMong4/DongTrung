using WebDongTrung.Datas;
namespace WebDongTrung.Repositories
{
    public interface ICartDetail
    {
        public Task<IEnumerable<CartDetail>> GetAllCartDetailAsync();
        public Task<int> GetCartDetailAsync();
        public Task<int> AddCartDetailAsync(int idCart, int idProduct, CartDetail cartDetail);
        public Task UpdateCartDetailAsync(CartDetail cartDetail);
        public Task DeleteCartDetailAsync(int idCart, int idProduct);
    }
}