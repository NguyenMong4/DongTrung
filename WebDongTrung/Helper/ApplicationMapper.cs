using AutoMapper;
using WebDongTrung.Datas;
using WebDongTrung.DTO.Cart;
using WebDongTrung.DTO.Product;
using WebDongTrung.Models;

namespace WebDongTrung.Helper
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            CreateMap<Product, ProductModel>().ReverseMap();
            CreateMap<Product, ProductCreateDto>().ReverseMap();
            CreateMap<Cart, CartModel>().ReverseMap();
            CreateMap<Cart, CartCreateDto>().ReverseMap();
            CreateMap<CartDetail, CartDetailModel>().ReverseMap();
            CreateMap<CartDetail, CartDetails>().ReverseMap();
            CreateMap<Employee, EmployeeModel>().ReverseMap();
            CreateMap<List<object>, CartDetailModel>().ReverseMap();
            CreateMap<List<object>, WarehouseModel>().ReverseMap();
            CreateMap<Warehouse, WarehouseModel>().ReverseMap();
            CreateMap<ImportBill, WarehouseModel>().ReverseMap();
            CreateMap<Advertisement, AdvertisementModel>().ReverseMap();
            CreateMap<Blog, BlogModel>().ReverseMap();
        }
    }
}