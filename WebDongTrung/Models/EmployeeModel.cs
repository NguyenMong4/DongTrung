using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebDongTrung.Datas;

namespace WebDongTrung.Models
{
    public class EmployeeModel
    {
        public string? UserName { get; set; }
        public string? PassWord { get; set; }
        [MaxLength(10)]
        public string? Phone { get; set; }
        [MaxLength(200)]
        public string? Email { get; set; }
        [MaxLength(200)]
        public string? Address { get; set; }
        public string? FullName { get; set; }
    }
}