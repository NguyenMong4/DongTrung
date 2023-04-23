using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebDongTrung.Datas
{
    public class Warehouse
    {
        [Key]
        [Column(Order = 1)]
        public string BillId { get; set; } = null!;
        public virtual ImportBill? Bill {get;set;}
        [Key]
        [Column(Order = 2)]
        public int ProductId { get; set; }
        public virtual Product? Product { get; set; }
        public int ImportQuantity { get; set; }
        public int? ImportPrice { get; set; }
    }
}