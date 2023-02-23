using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public CartClientController(ICartClient cartClient)
        {
            _cartClient = cartClient;
        }

        [HttpPost]
        public async Task<IActionResult> AddCartFromClient(CartModel cart)
        {
            var carts = _cartClient.AddCartFromClientAsync(cart);
            return carts != null ? Ok(carts): NotFound();
        }
    }
}