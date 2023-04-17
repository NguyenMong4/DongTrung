using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebDongTrung.Datas
{
    public class Advertisement:SystemProperties
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Photo { get; set; }
        public string? Position { get; set; }
        public string? Status { get; set; }
        public string? Description {get;set;}
    }
}