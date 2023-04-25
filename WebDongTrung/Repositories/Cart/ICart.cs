using WebDongTrung.Datas;
using WebDongTrung.DTO.Cart;

namespace WebDongTrung.Repositories
{
    public interface ICart
    {
         public Task<GetAllCartDto> GetAllCartAsync(string? search, int? page);
         public Task<Cart?> GetCartAsync(int id);
         public Task<int> AddCartAsync(CartCreateDto cartModel, string? username);
         public Task UpDateCartAsync(int id, CartCreateDto cartModel, string? username);
         public Task DeleteCartAsync(int id);
         public Task<Cart> UpdateStatusAsync(int id, string? userName, UpdateStatusDto statusDto);
    }
}