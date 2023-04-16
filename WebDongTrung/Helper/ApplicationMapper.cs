using AutoMapper;
using WebDongTrung.Datas;
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
            CreateMap<CartDetail, CartDetailModel>().ReverseMap();
            CreateMap<Employee, EmployeeModel>().ReverseMap();
            CreateMap<List<object>, CartDetailModel>().ReverseMap();
            CreateMap<List<object>, WarehouseModel>().ReverseMap();
            CreateMap<Warehouse, WarehouseModel>().ReverseMap();
            CreateMap<ImportBill, WarehouseModel>().ReverseMap();
            CreateMap<Advertisement, AdvertisementModel>().ReverseMap();
        }
    }
}