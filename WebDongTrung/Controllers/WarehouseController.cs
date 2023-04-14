using Microsoft.AspNetCore.Mvc;
using WebDongTrung.Datas;
using WebDongTrung.Models;
using WebDongTrung.Repositories;

namespace WebDongTrung.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WarehouseController : ControllerBase
    {
        private readonly IWarehouse _warehouse;

        public WarehouseController(IWarehouse warehouse)
        {
            _warehouse = warehouse;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllWarehouse(int? page)
        {
            try
            {
                return Ok(await _warehouse.GetAllImportBillAsync(page));
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetWarehouse(string id)
        {
            var warehouse = await _warehouse.GetWarehouseAsync(id);
            return warehouse == null ? NotFound() : Ok(warehouse);
        }

        [HttpPost]
        public async Task<ActionResult> AddWarehouse(WarehouseModel warehouse)
        {
            var newWarehouse = await _warehouse.AddWarehouseAsync(warehouse);
            var wareh = await _warehouse.GetWarehouseAsync(newWarehouse);
            return wareh == null ? NotFound() : Ok(wareh);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateWarehouse(string id, WarehouseModel warehouseModel)
        {
            try
            {
                await _warehouse.UpdateWarehouseAsync(id, warehouseModel);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWarehouse(string id)
        {
            try
            {
                await _warehouse.DeleteWarehouseAsync(id);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}