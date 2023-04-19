using Microsoft.AspNetCore.Mvc;
using WebDongTrung.Models;
using WebDongTrung.Repositories;

namespace WebDongTrung.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductTypeController : ControllerBase
    {
        private readonly IProductType _productType;

        public ProductTypeController(IProductType productType)
        {
            _productType = productType;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllProductType()
        {
            try
            {
                return Ok(await _productType.GetAllProductTypeAsync());
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductType(int id)
        {
            var productType = await _productType.GetProductTypeAsync(id);
            return productType == null ? NotFound() : Ok(productType);
        }

        [HttpPost]
        public async Task<ActionResult> AddProductType(ProductTypeModel productType)
        {
            try
            {
                var username = Request.Cookies["CookieUserName"];
                var newProductType = await _productType.AddProductTypeAsync(productType, username);
                var pdt = await _productType.GetProductTypeAsync(newProductType!);
                return pdt == null ? NotFound() : Ok(pdt);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProductType(int id, ProductTypeModel productType)
        {
            try
            {
                var username = Request.Cookies["CookieUserName"];
                await _productType.UpdateProductTypeAsync(id, productType, username);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductType(int id)
        {
            try
            {
                await _productType.DeleteProductTypeAsync(id);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }

        }
    }
}