using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebDongTrung.Datas;

namespace WebDongTrung.Models
{
    public class EmployeeModel : SystemProperties
    {
        public string? Id { get; set; }
        public string? UserName { get; set; }
        [MaxLength(10)]
        public string? Phone { get; set; }
        [MaxLength(200)]
        public string? Email { get; set; }
        [MaxLength(200)]
        public string? Address { get; set; }
        public string? Group { get; set; }
        public string? PassWord {get;set;}
    }
}