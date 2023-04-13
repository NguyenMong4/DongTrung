using System.Text.RegularExpressions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebDongTrung.Datas
{
    public class Employee : SystemProperties
    {
        [Key]
        [MaxLength(100)]
        public string Id { get; set; } = null!;
        public string? UserName { get; set; }
        [MaxLength(10)]
        public string? Phone { get; set; }
        [MaxLength(200)]
        public string? Email { get; set; }
        [MaxLength(200)]
        public string? Address { get; set; }
        public string? FullName { get; set; }
    }
}