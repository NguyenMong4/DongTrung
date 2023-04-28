using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Microsoft.EntityFrameworkCore;
using WebDongTrung.Datas;
using WebDongTrung.DTO.Cart;
using WebDongTrung.Models;

namespace WebDongTrung.Repositories.Advertisment
{
    public class AdvertisementRepo : IAdvertisment
    {
        private readonly StoreDbContex _context;
        private readonly IMapper _mapper;
        private readonly IHostEnvironment _env;

        public AdvertisementRepo(StoreDbContex contex, IMapper mapper, IHostEnvironment env)
        {
            _context = contex;
            _mapper = mapper;
            _env = env;
        }

        public async Task<int> AddAdvertisAsync(AdvertisementModel advertisementModel, string? username)
        {
            try
            {
                var adver = _mapper.Map<Advertisement>(advertisementModel);
                if (advertisementModel.PhotoFile != null)
                {
                    var directory = Path.Combine(_env.ContentRootPath, "wwwroot/images");
                    Directory.CreateDirectory(directory);
                    var filePath = Path.Combine(directory, advertisementModel.PhotoFile.FileName);
                    adver.Photo = $"wwwroot/images/{advertisementModel.PhotoFile.FileName}";
                    using FileStream fs = new(filePath, FileMode.Create);
                    advertisementModel.PhotoFile.CopyTo(fs);
                    fs.Close();
                }
                adver.CreateAt = DateTime.Now;
                adver.CreateId = username;
                adver.Status = "1";
                var id = _context.Advertisements!.Add(adver);
                await _context.SaveChangesAsync();
                return adver.Id;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public async Task DeleteAdvertisAsync(int id)
        {
            var adver = _context.Advertisements!.SingleOrDefault(ad => ad.Id == id);
            if (adver != null)
            {
                _context.Advertisements!.Remove(adver);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Advertisement>> GetAllAdvertisAsync()
        {
            var advertisements = await _context.Advertisements!.Where(x => x.Status!.Equals("1")).ToListAsync();
            return _mapper.Map<IEnumerable<Advertisement>>(advertisements);
        }

        public async Task<Advertisement> GetAdvertisAsync(int id)
        {
            var adver = await _context.Advertisements!.FindAsync(id);
            return _mapper.Map<Advertisement>(adver);
        }

        public async Task UpdateAdvertisAsync(int id, AdvertisementModel advertisementModel, string? username)
        {
            try
            {
                var adver = _mapper.Map<Advertisement>(advertisementModel);
                if (advertisementModel.PhotoFile != null)
                {
                    var directory = Path.Combine(_env.ContentRootPath, "wwwroot/images");
                    Directory.CreateDirectory(directory);
                    var filePath = Path.Combine(directory, advertisementModel.PhotoFile.FileName);
                    adver.Photo = $"wwwroot/images/{advertisementModel.PhotoFile.FileName}";
                    using FileStream fs = new(filePath, FileMode.Create);
                    advertisementModel.PhotoFile.CopyTo(fs);
                    fs.Close();
                }
                adver.Id = id;
                adver.UpdateAt = DateTime.Now;
                adver.UpdateId = username;
                _context.Advertisements!.Update(adver);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<Advertisement> UpdateStatusAsync(int id, string? userName, UpdateStatusDto status)
        {
            var advertisQuery = await GetAdvertisAsync(id);
            if (advertisQuery == null)
            {
                return advertisQuery;
            }
            advertisQuery.UpdateId = userName;
            advertisQuery.UpdateAt = DateTime.Now;
            JsonPatchDocument advDocument = new();
            advDocument.Operations.Add(new Operation
            {
                op = "replace",
                path = "Status",
                value = status.Status
            });
            advDocument.ApplyTo(advertisQuery);
            await _context.SaveChangesAsync();
            return advertisQuery;
        }
    }
}