using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebDongTrung.Datas;

namespace WebDongTrung.DTO.Product
{
    public class ProductGetAllDto
    {
        public int? MaxPage { get; set; }
        public List<Datas.Product>? Products { get; set; }
    }
}