using WebDongTrung.Datas;
using WebDongTrung.Models;

namespace WebDongTrung.Repositories
{
    public interface IWarehouse
    {
        public Task<List<ImportBill>> GetAllImportBillAsync(int? page = 1);
        public Task<WarehouseModel> GetWarehouseAsync(string id);
        public Task<string> AddWarehouseAsync(WarehouseModel warehouseModel);
        public Task UpdateWarehouseAsync(string id, WarehouseModel warehouseModel);
        public Task DeleteWarehouseAsync(string id);
    }
}