using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebDongTrung.Datas
{
    public abstract class SystemProperties
    {
        [MaxLength(12)]
        public string? CreateId { get; set; }
        public DateTime? CreateAt { get; set; }
        [MaxLength(12)]
        public string? UpdateId { get; set; }
        public DateTime? UpdateAt { get; set; }
    }
}