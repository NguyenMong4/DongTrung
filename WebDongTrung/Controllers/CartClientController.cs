using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebDongTrung.Datas;
using WebDongTrung.Models;
using WebDongTrung.Repositories.CartClient;

namespace WebDongTrung.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartClientController : ControllerBase
    {
        private readonly ICartClient _cartClient;
        private readonly IMapper _mapper;
        private readonly StoreDbContex _contex;
        public CartClientController(ICartClient cartClient, IMapper mapper, StoreDbContex contex)
        {
            _contex = contex;
            _cartClient = cartClient;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> AddCartFromClient(CartModel cartModel)
        {
            var cart = _mapper.Map<Cart>(cartModel);
            cart.CreateId = "KH";
            cart.UpdateId = "KH";
            await _contex.Carts!.AddAsync(cart);
            var idCart = cart.Id;
            foreach (var item in cartModel.CartDetailModel)
            {
                var cartDetail = _mapper.Map<CartDetail>(item);
                cartDetail.IdCart = idCart;
                cartDetail.CreateId = "KH";
                cartDetail.UpdateId = "KH";
                await _contex.CartDetails!.AddAsync(cartDetail);
            }
            await _contex.SaveChangesAsync();

            return cart != null ? Ok(cart) : NotFound();
        }
    }
}