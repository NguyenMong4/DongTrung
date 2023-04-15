using System.IO;
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
        private readonly IHostEnvironment _env;
        public static int PageSize { get; set; } = 9;

        public ProductRepositories(StoreDbContex context, IMapper mapper, IHostEnvironment env)
        {
            _contex = context;
            _mapper = mapper;
            _env = env;
        }

        public List<ProductModel> GetAllProduct(string? search, string? sortBy, int? productType, int? page = 1)
        {
            var allProduct = _contex.Products!.AsQueryable();
            //searching
            if (!string.IsNullOrEmpty(search))
            {
                allProduct = allProduct.Where(p => p.Name!.Contains(search));
            }

            if (productType != null)
            {
                allProduct = allProduct.Where(p => p.ProductTypeId == productType);
            }

            allProduct = allProduct.OrderBy(p => p.Name);

            //sorting
            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy)
                {
                    case "price_desc":
                        allProduct = allProduct.OrderByDescending(p => p.Price);
                        break;
                    case "price_asc":
                        allProduct = allProduct.OrderBy(p => p.Price);
                        break;
                }
            }

            //page
            if (page != null)
                allProduct = allProduct.Skip((int)((page - 1) * PageSize)).Take(PageSize);

            var result = allProduct.Select(p => new ProductModel
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Discount = p.Discount,
                Photo = p.Photo
            });

            return result.ToList();
        }

        public async Task<int> AddProductAsync(Product product)
        {
            try
            {
                if (product.PhotoFile != null)
                {
                    var directory = Path.Combine(_env.ContentRootPath, "wwwroot/images");
                    Directory.CreateDirectory(directory);
                    var filePath = Path.Combine(directory, product.PhotoFile.FileName);
                    product.Photo = $"wwwroot/images/{product.PhotoFile.FileName}";
                    using FileStream fs = new(filePath, FileMode.Create);
                    product.PhotoFile.CopyTo(fs);
                    fs.Close();
                }
                _contex.Products!.Add(product);
                await _contex.SaveChangesAsync();
                return product.Id;
            }
            catch (Exception)
            {
                return -1;
            }
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

        public async Task<IEnumerable<Product>> GetAllProductAsync()
        {
            var products = await _contex.Products!.ToListAsync();
            return _mapper.Map<IEnumerable<Product>>(products);
        }

        public async Task<Product> GetProductAsync(int id)
        {
            var product = await _contex.Products!.FindAsync(id);
            return _mapper.Map<Product>(product);
        }

        public async Task UpdateProductAsync(int id, Product model)
        {
            model.Id = id;
            var product = _mapper.Map<Product>(model);
            _contex.Products!.Update(product);
            await _contex.SaveChangesAsync();
        }

        public IEnumerable<ProductModel> GetProductsDiscount()
        {
            return _contex.Products!.Where(p => p.Discount != 0).Select(p => new ProductModel
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Photo = p.Photo,
                Discount = p.Discount
            });
        }
    }
}