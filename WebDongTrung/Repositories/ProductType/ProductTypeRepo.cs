using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebDongTrung.Datas;
using WebDongTrung.Models;

namespace WebDongTrung.Repositories
{
    public class ProductTypeRepo : IProductType
    {
        private readonly StoreDbContex _contex;
        private readonly IMapper _mapper;

        public ProductTypeRepo(StoreDbContex contex, IMapper mapper)
        {
            _contex = contex;
            _mapper = mapper;
        }

        public async Task<int> AddProductTypeAsync(ProductTypeModel productTypeModel, string? username)
        {
            var newProductType = _mapper.Map<ProductType>(productTypeModel);
            newProductType.CreateAt = DateTime.Now;
            newProductType.CreateId = username;
            _contex.ProductTypes!.Add(newProductType);
            await _contex.SaveChangesAsync();
            return newProductType.Id;
        }

        public async Task DeleteProductTypeAsync(int id)
        {
            var productType = _contex.ProductTypes!.SingleOrDefault(producttype => producttype.Id == id);
            if (productType != null)
            {
                _contex.ProductTypes!.Remove(productType);
                await _contex.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<ProductType>> GetAllProductTypeAsync()
        {
            var productType = await _contex.ProductTypes!.ToListAsync();
            return _mapper.Map<IEnumerable<ProductType>>(productType);
        }

        public async Task<ProductType> GetProductTypeAsync(int id)
        {
            var productType = await _contex.ProductTypes!.FindAsync(id);
            return _mapper.Map<ProductType>(productType);
        }

        public async Task UpdateProductTypeAsync(int id, ProductTypeModel productTypeModel, string? username)
        {
            var productTypes = _mapper.Map<ProductType>(productTypeModel);
            productTypes.Id = id;
            productTypes.UpdateAt = DateTime.Now;
            productTypes.UpdateId = username;
            _contex.ProductTypes!.Update(productTypes);
            await _contex.SaveChangesAsync();
        }
    }
}