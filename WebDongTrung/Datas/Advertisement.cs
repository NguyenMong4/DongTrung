using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebDongTrung.Datas
{
    public class Advertisement
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Photo { get; set; }
        public int IdProduct { get; set; }
        public virtual Product? Product{get;set;}
        public string? Position { get; set; }
        public string? Status { get; set; }
    }
}