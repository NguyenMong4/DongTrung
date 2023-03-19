using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebDongTrung.Models
{
    public class CartDetailModel
    {
        public int IdCart { get; set; }
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public string? Name { get; set; }
        public string? Photo { get; set; }
    }
}