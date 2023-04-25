using WebDongTrung.Models;
using WebDongTrung.Datas;
using AutoMapper;
using WebDongTrung.DTO.Product;

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

        public Task<ProductGetAllDto> GetAllProductAsync(string? search, string? sortBy, int? productType, int? page = 1)
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

            int max_page = allProduct.Count() / PageSize;
            int remainder = allProduct.Count() % PageSize;
            max_page = max_page < 1 ? 1 : (remainder == 0 ? max_page : max_page + 1);
            //page
            if (page != null)
                allProduct = allProduct.Skip((int)((page - 1) * PageSize)).Take(PageSize);
            var lstProducts = new ProductGetAllDto
            {
                MaxPage = max_page,
                Products = allProduct.Select(p => new Product
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Discount = p.Discount,
                    Photo = p.Photo,
                    ProductTypeId = p.ProductTypeId,
                    RealityQuantity = p.RealityQuantity,
                    SystemQuantity = p.SystemQuantity,
                    GeneralInformation = p.GeneralInformation,
                    Description = p.Description
                }).ToList()
            };

            return Task.FromResult(lstProducts);
        }

        public async Task<int> AddProductAsync(ProductCreateDto productCreate, string? username)
        {
            try
            {
                var product = _mapper.Map<Product>(productCreate);
                if (productCreate.PhotoFile != null)
                {
                    var directory = Path.Combine(_env.ContentRootPath, "wwwroot/images");
                    Directory.CreateDirectory(directory);
                    var filePath = Path.Combine(directory, productCreate.PhotoFile.FileName);
                    product.Photo = $"wwwroot/images/{productCreate.PhotoFile.FileName}";
                    using FileStream fs = new(filePath, FileMode.Create);
                    productCreate.PhotoFile.CopyTo(fs);
                    fs.Close();
                }
                product.CreateAt = DateTime.Now;
                product.CreateId = username;
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

        public async Task<Product> GetProductAsync(int id)
        {
            var product = await _contex.Products!.FindAsync(id);
            return _mapper.Map<Product>(product);
        }

        public async Task UpdateProductAsync(int id, ProductCreateDto productUpdate, string? username)
        {
            try
            {
                var product = _mapper.Map<Product>(productUpdate);
                if (productUpdate.PhotoFile != null)
                {
                    var directory = Path.Combine(_env.ContentRootPath, "wwwroot/images");
                    Directory.CreateDirectory(directory);
                    var filePath = Path.Combine(directory, productUpdate.PhotoFile.FileName);
                    product.Photo = $"wwwroot/images/{productUpdate.PhotoFile.FileName}";
                    using FileStream fs = new(filePath, FileMode.Create);
                    productUpdate.PhotoFile.CopyTo(fs);
                    fs.Close();
                }
                product.Id = id;
                product.UpdateAt = DateTime.Now;
                product.UpdateId = username;
                _contex.Products!.Update(product);
                await _contex.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
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