using WebDongTrung.Datas;
using WebDongTrung.DTO.Cart;
using WebDongTrung.Models;

namespace WebDongTrung.Repositories
{
    public interface ICart
    {
         public List<CartModel> GetAllCart(string? search, int? page);
         public Task<Cart?> GetCartAsync(int id);
         public Task<int> AddCartAsync(CartCreateDto cartModel, string? username);
         public Task UpDateCartAsync(int id, CartCreateDto cartModel, string? username);
         public Task DeleteCartAsync(int id);
    }
}