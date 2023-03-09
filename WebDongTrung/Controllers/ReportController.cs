using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebDongTrung.Datas;
using WebDongTrung.DTO;

namespace WebDongTrung.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly StoreDbContex _context;

        public ReportController(StoreDbContex contex)
        {
            _context = contex;
        }
        [HttpGet]
        public IActionResult ReportMonth(string from, string to)
        {
            var fromDate = DateTime.Parse(from);
            var toDate = DateTime.Parse(to);
            var cart = _context.Carts!.Where(p => (p.CreateAt >= fromDate) && (p.CreateAt <= toDate));
            ReportDto rp = new()
            {
                SuccessOrder = cart.Where(p => p.Status == 2).Sum(s => s.Status),
                RefunOrder = cart.Where(p => p.Status == 3).Sum(s => s.Status),
                Total = cart.Where(p => p.Status == 2).Sum(s => s.TotalPrice),
                ProductsOrderLst = _context.CartDetails!.Join(_context.Products!, cd => cd.IdProduct, p=>p.Id, (cd,p) => new {cd,p})
                .Where(p => (p.cd.CreateAt >= fromDate) && (p.cd.CreateAt <= toDate))
            .GroupBy(item => item.cd.IdProduct)
            .Select(gr => new ProductsOrder
            {
                Idproduct = gr.First().cd.IdProduct,
                Quantity = gr.Sum(q => q.cd.Quantity),
                ProductName = gr.First().p.Name,
                ProductPhoto = gr.First().p.Photo
            }).ToList()
            };
            return rp !=null ? Ok(rp) : NotFound();
        }
    }
}