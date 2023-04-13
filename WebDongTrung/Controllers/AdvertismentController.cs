using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebDongTrung.Datas;
using WebDongTrung.Repositories.Advertisment;

namespace WebDongTrung.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdvertismentController : ControllerBase
    {
         private readonly IAdvertisment _advertis;

        public AdvertismentController(IAdvertisment advertis)
        {
            _advertis = advertis;
        }

        [HttpGet]
        public async Task<IActionResult> GetAlladvertiss()
        {
            try
            {
                return Ok(await _advertis.GetAllAdvertisAsync());
            }
            catch
            {
                return BadRequest();
            }
        }
                [HttpGet("{id}")]
        public async Task<IActionResult> Getadvertis(int id)
        {
            var advertis = await _advertis.GetAdvertisAsync(id);
            return advertis == null ? NotFound() : Ok(advertis);
        }

        [HttpPost]
        public async Task<ActionResult> Addadvertis(Advertisement advertis)
        {
            try
            {
                var newadvertis = await _advertis.AddAdvertisAsync(advertis);
                var ad = await _advertis.GetAdvertisAsync(newadvertis!);
                return ad == null ? NotFound() : Ok(ad);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Updateadvertis(int id, Advertisement advertis)
        {
            try
            {
                await _advertis.UpdateAdvertisAsync(id, advertis);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Deleteadvertis(int id)
        {
            try
            {
                await _advertis.DeleteAdvertisAsync(id);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}