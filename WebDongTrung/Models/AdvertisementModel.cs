using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebDongTrung.Models
{
    public class AdvertisementModel
    {
        [NotMapped]
         public IFormFile? PhotoFile { get; set; }
        public string? Position { get; set; }
        public string? Status { get; set; }
    }
}