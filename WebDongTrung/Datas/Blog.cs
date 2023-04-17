using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebDongTrung.Datas
{
    public class Blog : SystemProperties
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Titel { get; set; }
        public string? Content { get; set; }
        public int? Type { get; set; }
        public string? Photo { get; set; }
    }
}