using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebDongTrung.Datas
{
    public class Warehouse
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public int IdProduct { get; set; }
        public int ImportQuantity { get; set; }
        public int ImportPrice { get; set; }
        public int RealityQuantity {get;set;}
        public int SystemQuantity {get;set;}
    }
}