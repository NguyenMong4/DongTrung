using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebDongTrung.DTO.WareHouseDto
{
    public class CreateWareHouseDto
    {
        public DateTime? Import_date { get; set; }
        public List<ProductWareHouses> ProductWarehouses { get; set; } = null!;
        public int? Total_price { get; set; }
    }
    public class ProductWareHouses{
        public int IdProduct { get; set; }
        public int ImportQuantity { get; set; }
        public int? ImportPrice { get; set; }
    }
}