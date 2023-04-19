using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebDongTrung.Datas;
using WebDongTrung.Models;

namespace WebDongTrung.Repositories.Advertisment
{
    public interface IAdvertisment
    {
        public Task<IEnumerable<Advertisement>> GetAllAdvertisAsync();
        public Task<Advertisement> GetAdvertisAsync(int id);
        public Task<int> AddAdvertisAsync(AdvertisementModel advertisementModel, string? username);
        public Task UpdateAdvertisAsync(int id, AdvertisementModel advertisementModel, string? username);
        public Task DeleteAdvertisAsync(int id);
    }
}