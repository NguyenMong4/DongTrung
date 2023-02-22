using WebDongTrung.Datas;

namespace WebDongTrung.Repositories
{
    public interface ICart
    {
         public Task<IEnumerable<Cart>> GetAllCartAsync();
         public Task<Cart?> GetCartAsync(int id);
         public Task<int> AddCartAsync(Cart cart);
         public Task UpDateCartAsync(int id, Cart cart);
         public Task DeleteCartAsync(int id);
    }
}