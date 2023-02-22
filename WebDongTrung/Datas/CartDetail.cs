using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebDongTrung.Datas
{
    public class CartDetail
    {
        [Key]
        [Column(Order =1)]
        public int IdCart { get; set; }
        [Key]
        [Column(Order =2)]
        [ForeignKey("Product")]
        public int IdProduct { get; set; }
        public int Quantity { get; set; }
        public virtual List<Cart> Cart {get;set;}
        public virtual List<Product> Products{get;set;}
    }
}