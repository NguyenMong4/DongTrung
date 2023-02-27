using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebDongTrung.Datas;

namespace WebDongTrung.Models
{
    public class WarehouseModel :SystemProperties
    {
        public string Id { get; set; } = null!;
        public int IdProduct { get; set; }
        public List<int> IdProductLst {get;set;} = null!;
        public int ImportQuantity { get; set; }
        public int? ImportPrice { get; set; }
        public int? RealityQuantity {get;set;}
        public int? SystemQuantity {get;set;}
    }
}