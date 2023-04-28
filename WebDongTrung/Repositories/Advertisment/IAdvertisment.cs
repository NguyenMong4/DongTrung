using WebDongTrung.Datas;
using WebDongTrung.DTO.Cart;
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
        public Task<Advertisement> UpdateStatusAsync(int id, string? userName, UpdateStatusDto statusDto);
    }
}