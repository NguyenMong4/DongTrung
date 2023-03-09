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
            CreateMap<Cart,CartModel>().ReverseMap();
            CreateMap<CartDetail, CartDetailModel>().ReverseMap();
            CreateMap<Employee, EmployeeModel>().ReverseMap();
            CreateMap<List<object>,CartDetailModel>().ReverseMap();
        }
    }
}