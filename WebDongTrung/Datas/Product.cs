﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebDongTrung.Datas
{
    public class Product : SystemProperties
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(2000)]
        public string? Name { get; set; }
        public int? ProductTypeId { get; set; }
        public string? Description { get; set; }
        public int? Price { get; set; }
        public int Discount { get; set; }
        public string? GeneralInformation { get; set; }
        public string? Photo { get; set; }
        [NotMapped]
        public IFormFile? PhotoFile {get;set;}
        public int? RealityQuantity {get;set;}
        public int? SystemQuantity {get;set;}
    }
}
