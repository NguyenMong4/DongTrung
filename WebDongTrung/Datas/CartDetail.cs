using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebDongTrung.Datas
{
    public class CartDetail
    {
        [Key]
        public int IdCart { get; set; }
        [Key]
        public int IdProduct { get; set; }
        public int Quantity { get; set; }
    }
}