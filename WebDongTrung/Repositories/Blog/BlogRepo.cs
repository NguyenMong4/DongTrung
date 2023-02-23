using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebDongTrung.Datas;

namespace WebDongTrung.Repositories
{
    public class BlogRepo : IBlog
    {
        private readonly StoreDbContex _context;
        private readonly IMapper _mapper;

        public BlogRepo(StoreDbContex contex, IMapper mapper)
        {
            _context = contex;
            _mapper = mapper;
        }

        public async Task<int> AddBlogAsync(Blog blog)
        {
            var newBlog = _mapper.Map<Blog>(blog);
            _context.Blogs!.Add(newBlog);
            await _context.SaveChangesAsync();
            return newBlog.Id;
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

        public async Task<IEnumerable<Blog>> GetAllBlogAsync()
        {
            var blogs = await _context.Blogs!.ToListAsync();
            return _mapper.Map<IEnumerable<Blog>>(blogs);
        }

        public async Task<Blog> GetBlogAsync(int id)
        {
            var blog = await _context.Products!.FindAsync(id);
            return _mapper.Map<Blog>(blog);
        }

        public async Task UpdateBlogAsync(int id, Blog blog)
        {
            if (id == blog.Id)
            {
                var blogs = _mapper.Map<Blog>(blog);
                _context.Blogs!.Update(blogs);
                await _context.SaveChangesAsync();
            }
        }
    }
}