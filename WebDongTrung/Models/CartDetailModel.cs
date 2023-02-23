using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebDongTrung.Models
{
    public class CartDetailModel
    {
        public int IdProduct { get; set; }
        public virtual ProductModel? ProductModel { get; set; }
        public int? Quantity { get; set; }
        public int? Price { get; set; }
    }
}