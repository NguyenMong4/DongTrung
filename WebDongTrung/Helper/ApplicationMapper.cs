using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using WebDongTrung.Datas;
using WebDongTrung.Models;

namespace WebDongTrung.Helper
{
    public class ApplicationMapper: Profile
    {
        public ApplicationMapper()
        {
            CreateMap<Product,ProductModel>().ReverseMap();
        }
    }
}