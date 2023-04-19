using WebDongTrung.Datas;
using WebDongTrung.Models;

namespace WebDongTrung.Repositories
{
    public interface IProductType
    {
        public Task<IEnumerable<ProductType>> GetAllProductTypeAsync();
        public Task<ProductType> GetProductTypeAsync(int id);
        public Task<int> AddProductTypeAsync(ProductTypeModel productTypeModel, string? username);
        public Task UpdateProductTypeAsync(int id, ProductTypeModel productTypeModel, string? username);
        public Task DeleteProductTypeAsync(int id);
    }
}