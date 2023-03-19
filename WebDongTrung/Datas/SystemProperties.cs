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
        public string CreateId { get; set; } = null!;
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        [MaxLength(12)]
        public string UpdateId { get; set; } = null!;
        public DateTime UpdateAt { get; set; } = DateTime.UtcNow;
    }
}