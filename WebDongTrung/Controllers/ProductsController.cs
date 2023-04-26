using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using WebDongTrung.Datas;
using WebDongTrung.DTO.Cart;
using WebDongTrung.DTO.Product;
using WebDongTrung.Models;
using WebDongTrung.Repositories;

namespace WebDongTrung.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProducts _product;
        private readonly IConfiguration _configuration;

        public ProductsController(IProducts product, IConfiguration configuration)
        {
            _product = product;
            _configuration = configuration;

        }
        [HttpGet]
        public async Task<IActionResult> GetAllProduct(string? search, string? sortBy, int? productType, int? page)
        {
            try
            {
                var connectionString = _configuration.GetConnectionString("DefaultConnect");
                Console.WriteLine(connectionString);
                return Ok(await _product.GetAllProductAsync(search, sortBy, productType, page));
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("Discount")]
        public IActionResult GetAllProductDiscount()
        {
            return Ok(_product.GetProductsDiscount());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _product.GetProductAsync(id);
            return product == null ? NotFound() : Ok(product);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult> AddProduct([FromForm] ProductCreateDto productModel)
        {
            try
            {
                var username = Request.Cookies["CookieUserName"];
                var newProduct = await _product.AddProductAsync(productModel, username);
                var product = await _product.GetProductAsync(newProduct);
                return product == null ? NotFound() : Ok(product);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UpdateProduct(int id, [FromForm] ProductCreateDto productModel)
        {
            try
            {
                var username = Request.Cookies["CookieUserName"];
                await _product.UpdateProductAsync(id, productModel, username);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                await _product.DeleteProductAsync(id);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateQuantityProduct(int id, [FromBody] UpdateQuantityDto quantityDto)
        {
            try
            {
                var username = Request.Cookies["CookieUserName"];
                var updateQuantity = await _product.UpdateQuantityAsync(id, username, quantityDto);
                if (updateQuantity == null)
                    return NotFound();
                return Ok(updateQuantity);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}