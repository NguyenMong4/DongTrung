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
        public static int PageSize {get;set;} = 1;

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

        public List<CartModel> GetAllCart(string? search, int page = 1)
        {
            var allCart = _contex.Carts!.AsQueryable();
            //searching
            if (!string.IsNullOrEmpty(search))
            {
                allCart = allCart.Where(p => p.Phone!.Contains(search));
            }
            allCart = allCart.OrderByDescending(p => p.CreateAt);
            allCart = allCart.Skip((page - 1)* PageSize).Take(PageSize);

            var result = allCart.Select(p => new CartModel
            {
                Id = p.Id,
                TotalPrice = p.TotalPrice,
                Status = p.Status,
                Address = p.Address,
                Payment = p.Payment,
                PersonName = p.PersonName,
            });
            return result.ToList();
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
                    var product = _contex.Products!.SingleOrDefault(p => p.Id == item.Id);
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