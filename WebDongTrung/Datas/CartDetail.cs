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
        public virtual Cart? Cart{get;set;}
        [Key]
        [Column(Order =2)]
        [ForeignKey("Product")]
        public int IdProduct { get; set; }
        public virtual Product? Product{get;set;}
        public int Quantity { get; set; }
    }
}