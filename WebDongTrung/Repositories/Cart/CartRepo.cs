using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebDongTrung.Datas;

namespace WebDongTrung.Repositories
{
    public class CartRepo : ICart
    {
        private readonly StoreDbContex _contex;
        private readonly IMapper _mapper;

        public CartRepo(StoreDbContex contex, IMapper mapper)
        {
            _contex = contex;
            _mapper = mapper;
        }
        public async Task<int> AddCartAsync(Cart cart)
        {
            _contex.Carts.AddAsync(cart);
            await _contex.SaveChangesAsync();
            return cart.Id;

        }

        public async Task DeleteCartAsync(int id)
        {
            var cart = _contex.Carts!.SingleOrDefault(c => c.Id == id);
            if (cart != null)
            {
                _contex.Carts.Remove(cart);
                await _contex.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Cart>> GetAllCartAsync()
        {
            var cart = await _contex.Carts!.ToListAsync();
            return _mapper.Map<IEnumerable<Cart>>(cart);
        }

        public async Task<Cart?> GetCartAsync(int id)
        {
            return await _contex.Carts!.FindAsync(id);
        }

        public async Task UpDateCartAsync(int id, Cart cart)
        {
            if (id == cart.Id)
            {
                 _contex.Carts!.Update(cart);
                await _contex.SaveChangesAsync();
            }
        }
    }
}