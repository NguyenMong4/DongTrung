using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebDongTrung.Models
{
    public class ProductWarehouseModel
    {
        // public string? IdBill { get; set; } = null!;
        public int ProductId { get; set; }
        public string? NameProduct { get; set; }
        public int ImportQuantity { get; set; }
        public int? ImportPrice { get; set; }
    }
}