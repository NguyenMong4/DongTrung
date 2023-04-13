using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebDongTrung.Datas;

namespace WebDongTrung.Repositories
{
    public interface IBlog
    {
        public Task<IEnumerable<Blog>> GetAllBlogAsync(int type);
        public Task<Blog> GetBlogAsync(int id);
        public Task<int> AddBlogAsync(Blog blog);
        public Task UpdateBlogAsync(int id, Blog blog);
        public Task DeleteBlogAsync(int id);
    }
}