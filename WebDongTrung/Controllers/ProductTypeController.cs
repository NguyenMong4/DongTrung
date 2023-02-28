using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebDongTrung.Datas;
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
        public async Task<ActionResult> AddProductType(ProductType productType)
        {
            try
            {
                var newProductType = await _productType.AddProductTypeAsync(productType);
                var pdt = await _productType.GetProductTypeAsync(newProductType!);
                return pdt == null ? NotFound() : Ok(pdt);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProductType(int id, ProductType productType)
        {
            try
            {
                await _productType.UpdateProductTypeAsync(id, productType);
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