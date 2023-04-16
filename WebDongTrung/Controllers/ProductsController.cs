using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebDongTrung.Datas;
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

        public ProductsController(IProducts product)
        {
            _product = product;
        }
        [HttpGet]
        public IActionResult GetAllProduct(string? search, string? sortBy, int? productType, int? page)
        {
            try
            {
                return Ok(_product.GetAllProduct(search, sortBy,productType, page));
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("Discount")]
        public IActionResult GetAllProductDiscount(){
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
        public async Task<ActionResult> AddProduct([FromForm]ProductCreateDto productModel)
        {
            try
            {
                var newProduct = await _product.AddProductAsync(productModel);
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
        public async Task<IActionResult> UpdateProduct(int id,[FromForm] ProductCreateDto productModel)
        {
            try
            {
                await _product.UpdateProductAsync(id, productModel);
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
    }
}