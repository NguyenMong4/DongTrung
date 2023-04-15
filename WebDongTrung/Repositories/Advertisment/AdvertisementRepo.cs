using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebDongTrung.Datas;

namespace WebDongTrung.Repositories.Advertisment
{
    public class AdvertisementRepo : IAdvertisment
    {
        private readonly StoreDbContex _context;
        private readonly IMapper _mapper;

        public AdvertisementRepo(StoreDbContex contex, IMapper mapper)
        {
            _context = contex;
            _mapper = mapper;
        }

        public async Task<int> AddAdvertisAsync(Advertisement adver)
        {
            _context.Advertisements!.Add(adver);
            await _context.SaveChangesAsync();
            return adver.Id;
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
            return await _context.Advertisements!.ToListAsync();
        }

        public async Task<Advertisement> GetAdvertisAsync(int id)
        {
            var adver = await _context.Advertisements!.FindAsync(id);
            return _mapper.Map<Advertisement>(adver);
        }

        public async Task UpdateAdvertisAsync(int id, Advertisement adver)
        {
            adver.Id = id;
            _context.Advertisements!.Update(adver);
            await _context.SaveChangesAsync();
        }
    }
}