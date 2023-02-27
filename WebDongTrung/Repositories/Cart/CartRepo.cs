using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebDongTrung.Datas;
using WebDongTrung.Models;

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

        public async Task UpDateCartAsync(int id, CartModel cartModel)
        {
            if (id == cartModel.Id)
            {
                var cart = _mapper.Map<Cart>(cartModel);
                cart.UpdateAt = DateTime.Now;
                _contex.Carts!.Update(cart);
                foreach (var item in cartModel.CartDetailModel)
                {
                    var product = _contex.Products!.SingleOrDefault(p => p.Id == item.IdProduct);
                    if (product != null)
                    {
                        //trạng thái : đang giao hàng
                        if (cart.Status == 1)
                        {
                            product.RealityQuantity -= item.Quantity;
                        }
                        else if (cart.Status == 2) // trạng thái: giao hàng thành công
                        {
                            product.SystemQuantity -= item.Quantity;
                        }
                        else if(cart.Status == 3) //trạng thái : Hoàn đơn
                        {
                            product.RealityQuantity += item.Quantity;
                        }
                        _contex.Products!.Update(product);
                    }
                }
                await _contex.SaveChangesAsync();
            }
        }
    }
}