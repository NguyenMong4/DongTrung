
using Microsoft.AspNetCore.Mvc;
using WebDongTrung.Datas;
using WebDongTrung.Repositories;

namespace WebDongTrung.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartsController : ControllerBase
    {
        private readonly ICart _cart;
        public CartsController(ICart cart)
        {
            _cart = cart;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCart()
        {
            try
            {
                return Ok(await _cart.GetAllCartAsync());
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCart(int id)
        {
            var cart = await _cart.GetCartAsync(id);
            return cart != null ? Ok(cart) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> AddCart(Cart cart)
        {
            try
            {
                var newCart = await _cart.AddCartAsync(cart);
                var cartnew = await _cart.GetCartAsync(newCart);
                return cartnew != null ? Ok(cartnew) : NotFound();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCart(int id, Cart cart)
        {
            try
            {
                await _cart.UpDateCartAsync(id, cart);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }

        }
    }
}