using AutoMapper;
using WebDongTrung.Datas;
using WebDongTrung.DTO.Cart;
using WebDongTrung.DTO.Product;
using WebDongTrung.DTO.WareHouseDto;
using WebDongTrung.Models;

namespace WebDongTrung.Helper
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            CreateMap<Product, ProductModel>().ReverseMap();
            CreateMap<Product, ProductCreateDto>().ReverseMap();
            CreateMap<ProductType, ProductTypeModel>().ReverseMap();
            CreateMap<Cart, CartModel>().ReverseMap();
            CreateMap<Cart, CartCreateDto>().ReverseMap();
            CreateMap<CartDetail, CartDetailModel>().ReverseMap();
            CreateMap<CartDetail, CartDetails>().ReverseMap();
            CreateMap<Employee, EmployeeModel>().ReverseMap();
            CreateMap<List<object>, CartDetailModel>().ReverseMap();
            CreateMap<List<object>, ImportBillModel>().ReverseMap();
            CreateMap<Warehouse, ProductWarehouseModel>().ReverseMap();
            CreateMap<Cart, UpdateStatusDto>().ReverseMap();
            CreateMap<ImportBill, ImportBillModel>().ReverseMap();
            CreateMap<ImportBill, CreateWareHouseDto>().ReverseMap();
            CreateMap<Advertisement, AdvertisementModel>().ReverseMap();
            CreateMap<Blog, BlogModel>().ReverseMap();
        }
    }
}