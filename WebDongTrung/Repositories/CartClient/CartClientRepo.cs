using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using WebDongTrung.Datas;
using WebDongTrung.Models;

namespace WebDongTrung.Repositories.CartClient
{

    public class CartClientRepo : ICartClient
    {
        private readonly StoreDbContex _contex;
        private readonly IMapper _mapper;
        public CartClientRepo(StoreDbContex contex, IMapper mapper)
        {
            _contex = contex;
            _mapper = mapper;
        }

        public async Task<int> AddCartFromClientAsync(CartModel cartModel)
        {
            var cart = _mapper.Map<Cart>(cartModel);
            _contex.Carts!.AddAsync(cart);
            var idCart = cart.Id;
            var cartDetail = _mapper.Map<CartDetail>(cartModel.ProductModel);
            cartDetail.IdCart = idCart;
            _contex.CartDetails!.AddAsync(cartDetail);
            await _contex.SaveChangesAsync();
            return cartDetail.IdCart;
        }
    }
}