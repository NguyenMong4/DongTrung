using WebDongTrung.Datas;
using WebDongTrung.Models;

namespace WebDongTrung.Repositories
{
    public interface ICart
    {
         public List<CartModel> GetAllCart(string? search, int page);
         public Task<Cart?> GetCartAsync(int id);
         public Task<int> AddCartAsync(CartModel cartModel);
         public Task UpDateCartAsync(int id, CartModel cartModel);
         public Task DeleteCartAsync(int id);
    }
}