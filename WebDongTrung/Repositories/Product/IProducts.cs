using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebDongTrung.Datas;
using WebDongTrung.DTO.Product;
using WebDongTrung.Models;

namespace WebDongTrung.Repositories
{
    public interface IProducts
    {
        public Task<Product> GetProductAsync(int id);
        public Task<int> AddProductAsync(ProductCreateDto product, string? username);
        public Task UpdateProductAsync(int id, ProductCreateDto productUpdate, string? username);
        public Task DeleteProductAsync(int id);
        public List<Product> GetAllProduct(string? search,string? sortBy, int? productType, int? page);
        public IEnumerable<ProductModel> GetProductsDiscount();

    }
}