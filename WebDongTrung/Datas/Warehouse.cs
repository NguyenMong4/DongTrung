using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebDongTrung.Datas
{
    public class Warehouse
    {
        [Key]
        [Column(Order = 1)]
        public string IdBill { get; set; } = null!;
        public virtual ImportBill? Bill {get;set;}
        [Key]
        [Column(Order = 2)]
        public int IdProduct { get; set; }
        public virtual Product? Product { get; set; }
        public int ImportQuantity { get; set; }
        public int? ImportPrice { get; set; }
    }
}