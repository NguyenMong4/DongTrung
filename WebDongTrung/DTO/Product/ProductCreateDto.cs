using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebDongTrung.DTO.Product
{
    public class ProductCreateDto
    {
        public string? Name { get; set; }
        public int? ProductTypeId { get; set; }
        public string? Description { get; set; }
        public int? Price { get; set; }
        public int Discount { get; set; }
        public string? GeneralInformation { get; set; }
        [NotMapped]
        public IFormFile? PhotoFile { get; set; }
        public string? Photo { get; set; }
        public int? RealityQuantity { get; set; }
    }
}