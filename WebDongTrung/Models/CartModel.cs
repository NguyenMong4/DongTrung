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
        public string Phone { get; set; }
        public string Address { get; set; }
        public string? Note { get; set; }
        public int Payment { get; set; }
        public string PersonName { get; set; }
        public DateTime ReceivedDate {get;set;} = DateTime.Now.AddDays(4);
        public CartDetailModel CartDetailModel {get;set;}
    }
}