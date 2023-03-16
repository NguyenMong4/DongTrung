using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebDongTrung.Datas;

namespace WebDongTrung.Models
{
    public class WarehouseModel
    {
        public List<ProductWarehouse> ProductWarehouses { get; set; } = null!;
    }
    public class ProductWarehouse
    {
        public int IdProduct { get; set; }
        public int ImportQuantity { get; set; }
        public int? ImportPrice { get; set; }
        public int? RealityQuantity { get; set; }
        public int? SystemQuantity { get; set; }
    }
}