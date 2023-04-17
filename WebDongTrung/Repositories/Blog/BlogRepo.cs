using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebDongTrung.Datas;
using WebDongTrung.Models;

namespace WebDongTrung.Repositories
{
    public class BlogRepo : IBlog
    {
        private readonly StoreDbContex _context;
        private readonly IMapper _mapper;
        private readonly IHostEnvironment _env;

        public BlogRepo(StoreDbContex contex, IMapper mapper, IHostEnvironment env)
        {
            _context = contex;
            _mapper = mapper;
            _env = env;
        }

        public async Task<int> AddBlogAsync(BlogModel blogModel)
        {
            try
            {
                var newBlog = _mapper.Map<Blog>(blogModel);
                if (blogModel.PhotoFile != null)
                {
                    var directory = Path.Combine(_env.ContentRootPath, "wwwroot/images");
                    Directory.CreateDirectory(directory);
                    var filePath = Path.Combine(directory, blogModel.PhotoFile.FileName);
                    newBlog.Photo = $"wwwroot/images/{blogModel.PhotoFile.FileName}";
                    using FileStream fs = new(filePath, FileMode.Create);
                    blogModel.PhotoFile.CopyTo(fs);
                    fs.Close();
                }
                newBlog.CreateAt = DateTime.Now;
                newBlog.CreateId = "admin";
                _context.Blogs!.Add(newBlog);
                await _context.SaveChangesAsync();
                return newBlog.Id;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public async Task DeleteBlogAsync(int id)
        {
            var blog = _context.Blogs!.SingleOrDefault(blog => blog.Id == id);
            if (blog != null)
            {
                _context.Blogs!.Remove(blog);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Blog>> GetAllBlogAsync(int type)
        {
            var blogs = await _context.Blogs!.Where(blog => blog.Type == type).ToListAsync();
            return _mapper.Map<IEnumerable<Blog>>(blogs);
        }

        public async Task<Blog> GetBlogAsync(int id)
        {
            var blog = await _context.Blogs!.FindAsync(id);
            return _mapper.Map<Blog>(blog);
        }

        public async Task UpdateBlogAsync(int id, BlogModel blogModel)
        {
            try
            {
                var blog = _mapper.Map<Blog>(blogModel);
                blog.Id = id;
                if (blogModel.PhotoFile != null)
                {
                    var directory = Path.Combine(_env.ContentRootPath, "wwwroot/images");
                    Directory.CreateDirectory(directory);
                    var filePath = Path.Combine(directory, blogModel.PhotoFile.FileName);
                    blog.Photo = $"wwwroot/images/{blogModel.PhotoFile.FileName}";
                    using FileStream fs = new(filePath, FileMode.Create);
                    blogModel.PhotoFile.CopyTo(fs);
                    fs.Close();
                }
                blog.UpdateAt = DateTime.Now;
                blog.UpdateId = "admin";
                _context.Blogs!.Update(blog);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}