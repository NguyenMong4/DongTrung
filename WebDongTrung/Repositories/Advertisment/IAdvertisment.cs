using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebDongTrung.Datas;

namespace WebDongTrung.Repositories.Advertisment
{
    public interface IAdvertisment
    {
        public Task<IEnumerable<Advertisement>> GetAllAdvertisAsync();
        public Task<Advertisement> GetAdvertisAsync(int id);
        public Task<int> AddAdvertisAsync(Advertisement blog);
        public Task UpdateAdvertisAsync(int id, Advertisement blog);
        public Task DeleteAdvertisAsync(int id);
    }
}