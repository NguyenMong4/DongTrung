using System.ComponentModel.DataAnnotations;
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
        public int ProductType { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public int Discount { get; set; }
        public string GeneralInformation { get; set; }
        public string Photo { get; set; }

    }
}
