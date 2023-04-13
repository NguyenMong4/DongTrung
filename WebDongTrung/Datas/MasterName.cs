using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebDongTrung.Datas
{
    public class MasterName:SystemProperties
    {
        [Key]
        [Column(Order = 1)]
        [MaxLength(12)]
        public string Code { get; set; } = null!;
        [Key]
        [Column(Order = 2)]
        [MaxLength(10)]
        public string Cd {get;set;} = null!;
        public string? Name { get; set; }
    }
}