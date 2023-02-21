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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [MaxLength(12)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string PassWord { get; set; }
        [MaxLength(10)]
        public string Phone { get; set; }
        [MaxLength(200)]
        public string Email { get; set; }
        [MaxLength(200)]
        public string Address { get; set; }
        public string Emp_kbn { get; set; }

    }
}