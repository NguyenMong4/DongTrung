using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebDongTrung.Datas;

namespace WebDongTrung.Models
{
    public class CartModel : SystemProperties
    {
        public int Id { get; set; }
        public int TotalPrice { get; set; }
        public int Status { get; set; }
        public string Phone { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string? Note { get; set; }
        public int Payment { get; set; }
        public string PersonName { get; set; } = null!;
        public DateTime ReceivedDate {get;set;}
        public List<CartDetailModel> CartDetailModel {get;set;} = null!;
    }
}