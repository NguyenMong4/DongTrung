using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebDongTrung.Datas;
using WebDongTrung.Models;

namespace WebDongTrung.Repositories
{
    public interface IWarehouse
    {
        public Task<IEnumerable<Warehouse>> GetAllWarehouseAsync();
        public Task<IEnumerable<Warehouse>> GetWarehouseAsync(string id);
        public Task<string> AddWarehouseAsync(WarehouseModel warehouse);
        public Task UpdateWarehouseAsync(string id, Warehouse warehouse);
        public Task DeleteWarehouseAsync(string id);
    }
}