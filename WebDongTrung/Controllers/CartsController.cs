
using Microsoft.AspNetCore.Mvc;
using WebDongTrung.Datas;
using WebDongTrung.DTO.Cart;
using WebDongTrung.Models;
using WebDongTrung.Repositories;

namespace WebDongTrung.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartsController : ControllerBase
    {
        private readonly ICart _cart;
        private readonly ICartDetail _cartDetail;
        public CartsController(ICart cart, ICartDetail cartDetail)
        {
            _cart = cart;
            _cartDetail = cartDetail;
        }

        [HttpGet]
        public IActionResult GetAllCarts(string? search, int? page)
        {
           try
            {
                return Ok(_cart.GetAllCart(search, page));
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCartDetail(int id)
        {
            var cart = await _cartDetail.GetCartDetailAsync(id);
            return cart != null ? Ok(cart) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> AddCart(CartCreateDto cart)
        {
            try
            {
                var username = Request.Cookies["CookieUserName"];
                var newCart = await _cart.AddCartAsync(cart, username);
                var cartnew = await _cart.GetCartAsync(newCart);
                return cartnew != null ? Ok(cartnew) : NotFound();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCart(int id, CartCreateDto cart)
        {
            try
            {
                var username = Request.Cookies["CookieUserName"];
                await _cart.UpDateCartAsync(id, cart, username);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPatch]
         public async Task<IActionResult> UpdateStatusCart(UpdateStatusDto updateStatusDto)
        {
            try
            {
                var username = Request.Cookies["CookieUserName"];
                await _cart.UpdateStatusAsync(updateStatusDto, username);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}