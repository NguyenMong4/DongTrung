using WebDongTrung.Datas;
using WebDongTrung.Models;

namespace WebDongTrung.Repositories
{
    public interface ICartDetail
    {
        public Task<IEnumerable<CartDetail>> GetAllCartDetailAsync();
        public Task<List<CartDetailModel>> GetCartDetailAsync(int id);
        public Task<int> AddCartDetailAsync(int idCart, int idProduct, CartDetail cartDetail);
        public Task UpdateCartDetailAsync(CartDetail cartDetail);
        public Task DeleteCartDetailAsync(int idCart, int idProduct);
    }
}