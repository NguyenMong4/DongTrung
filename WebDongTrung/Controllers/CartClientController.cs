using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebDongTrung.Datas;
using WebDongTrung.Models;

namespace WebDongTrung.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartClientController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly StoreDbContex _contex;
        public CartClientController(IMapper mapper, StoreDbContex contex)
        {
            _contex = contex;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> AddCartFromClient(CartModel cartModel)
        {
            var cart = _mapper.Map<Cart>(cartModel);
            cart.CreateId = "KH";
            cart.CreateAt = DateTime.Now;
            await _contex.Carts!.AddAsync(cart);
            await _contex.SaveChangesAsync();
            var idCart = cart.Id;
            foreach (var item in cartModel.CartDetailModel)
            {
                var cartDetail = _mapper.Map<CartDetail>(item);
                cartDetail.IdCart = idCart;
                cartDetail.CreateId = "KH";
                cartDetail.CreateAt = DateTime.Now;
                await _contex.CartDetails!.AddAsync(cartDetail);
                await _contex.SaveChangesAsync();
            }

            return cart != null ? Ok(cart) : NotFound();
        }
    }
}