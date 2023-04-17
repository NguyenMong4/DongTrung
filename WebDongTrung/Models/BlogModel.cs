using System.ComponentModel.DataAnnotations.Schema;

namespace WebDongTrung.Models
{
    public class BlogModel
    {
        public string? Titel { get; set; }
        public string? Content { get; set; }
        public int? Type { get; set; }
        [NotMapped]
        public IFormFile? PhotoFile { get; set; }
    }
}