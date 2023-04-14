using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebDongTrung.Datas
{
    public class ImportBill : SystemProperties
    {
        [Key]
        public string Id { get; set; } = null!;
        public DateTime? Import_date { get; set; }
        public int? Total_price { get; set; }
    }
}