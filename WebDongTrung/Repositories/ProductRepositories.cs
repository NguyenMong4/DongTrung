using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebDongTrung.Models;
using WebDongTrung.Datas;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace WebDongTrung.Repositories
{
    public class ProductRepositories : IProducts
    {
        private readonly StoreDbContex _contex;
        private readonly IMapper _mapper;

        public ProductRepositories(StoreDbContex context, IMapper mapper)
        {
            _contex = context;
            _mapper = mapper;
        }
        public async Task<int> AddProductAsync(ProductModel model)
        {
            var newProduct = _mapper.Map<Product>(model);
            _contex.Products!.Add(newProduct);
            await _contex.SaveChangesAsync();
            return newProduct.Id;
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = _contex.Products!.SingleOrDefault(product => product.Id == id);
            if (product != null)
            {
                _contex.Products!.Remove(product);
                await _contex.SaveChangesAsync();
            }
        }

        public async Task<List<ProductModel>> GetAllProductAsync()
        {
            var products = await _contex.Products!.ToListAsync();
            return _mapper.Map<List<ProductModel>>(products);
        }

        public async Task<ProductModel> GetProductAsync(int id)
        {
            var product = await _contex.Products!.FindAsync(id);
            return _mapper.Map<ProductModel>(product);
        }

        public async Task UpdateProductAsync(int id, ProductModel model)
        {
            if (id == model.Id)
            {
                var product = _mapper.Map<Product>(model);
                _contex.Products!.Update(product);
                await _contex.SaveChangesAsync();
            }
        }

        public async Task<decimal?> TongPriceAsync()
        {
            var products = await GetAllProductAsync();
            decimal? price = null;
            foreach (ProductModel s in products)
            {
                price += s.Price;
            }
            return price;
        }
    }
}