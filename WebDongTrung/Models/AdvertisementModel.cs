using System.ComponentModel.DataAnnotations.Schema;

namespace WebDongTrung.Models
{
    public class AdvertisementModel
    {
        [NotMapped]
         public IFormFile? PhotoFile { get; set; }
        public string? Photo { get; set; }
        public string? Position { get; set; }
        public string? Status { get; set; }
        public string? Description {get;set;}
    }
}