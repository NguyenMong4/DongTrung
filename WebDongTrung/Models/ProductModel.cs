using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebDongTrung.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        [MaxLength(2000)]
        public string? Name { get; set; }
        public int ProductType { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public int Discount { get; set; }
        public string GeneralInformation { get; set; }
        public string Photo { get; set; }
    }
}