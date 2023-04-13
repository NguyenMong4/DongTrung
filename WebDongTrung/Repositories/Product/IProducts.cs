using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebDongTrung.Datas;
using WebDongTrung.Models;

namespace WebDongTrung.Repositories
{
    public interface IProducts
    {
        public Task<IEnumerable<Product>> GetAllProductAsync();
        public Task<Product> GetProductAsync(int id);
        public Task<int> AddProductAsync(Product product);
        public Task UpdateProductAsync(int id, Product product);
        public Task DeleteProductAsync(int id);
        public List<ProductModel> GetAllProduct(string? search,string? sortBy, int? productType, int? page);
        public IEnumerable<ProductModel> GetProductsDiscount();

    }
}