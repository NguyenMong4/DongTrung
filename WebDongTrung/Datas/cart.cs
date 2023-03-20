using System.Security.Cryptography.X509Certificates;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebDongTrung.Datas
{
    public class Cart : SystemProperties
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int TotalPrice { get; set; }
        public int Status { get; set; }
        public string Phone { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string? Note { get; set; }
        public int Payment { get; set; }
        public string PersonName { get; set; } = null!;
        public DateTime ReceivedDate {get;set;}
        public string Email {get;set;} = null!;
    }
}