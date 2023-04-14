using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebDongTrung.Datas
{
    public class Warehouse
    {
        [Key]
        [Column(Order = 1)]
        public string IdBill { get; set; } = null!;
        [Key]
        [Column(Order = 2)]
        public int IdProduct { get; set; }
        public int ImportQuantity { get; set; }
        public int? ImportPrice { get; set; }
        public int? RealityQuantity {get;set;}
        public int? SystemQuantity {get;set;}
    }
}