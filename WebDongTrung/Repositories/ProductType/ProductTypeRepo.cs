using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebDongTrung.Datas;

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

        public async Task<int> AddProductTypeAsync(ProductType productType)
        {
             var newProductType = _mapper.Map<ProductType>(productType);
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

        public async Task UpdateProductTypeAsync(int id, ProductType productType)
        {
            if (id == productType.Id)
            {
                var productTypes = _mapper.Map<ProductType>(productType);
                _contex.ProductTypes!.Update(productTypes);
                await _contex.SaveChangesAsync();
            }
        }
    }
}