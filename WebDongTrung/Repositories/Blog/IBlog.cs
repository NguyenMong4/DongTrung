using WebDongTrung.Datas;
using WebDongTrung.Models;

namespace WebDongTrung.Repositories
{
    public interface IBlog
    {
        public Task<IEnumerable<Blog>> GetAllBlogAsync(int type);
        public Task<Blog> GetBlogAsync(int id);
        public Task<int> AddBlogAsync(BlogModel blogModel);
        public Task UpdateBlogAsync(int id, BlogModel blogModel);
        public Task DeleteBlogAsync(int id);
    }
}