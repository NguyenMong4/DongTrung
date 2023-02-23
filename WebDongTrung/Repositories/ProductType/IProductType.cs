using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebDongTrung.Datas;

namespace WebDongTrung.Repositories
{
    public interface IProductType
    {
        public Task<IEnumerable<ProductType>> GetAllProductTypeAsync();
        public Task<ProductType> GetProductTypeAsync(int id);
        public Task<int> AddProductTypeAsync(ProductType productType);
        public Task UpdateProductTypeAsync(int id, ProductType productType);
        public Task DeleteProductTypeAsync(int id);
    }
}