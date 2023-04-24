using WebDongTrung.Datas;
using WebDongTrung.DTO.WareHouseDto;
using WebDongTrung.Models;

namespace WebDongTrung.Repositories
{
    public interface IWarehouse
    {
        public Task<List<ImportBill>> GetAllImportBillAsync(int? page = 1);
        public Task<ImportBillModel> GetWarehouseAsync(string id);
        public Task<string> AddWarehouseAsync(CreateWareHouseDto newWarehouse, string? username);
        public Task UpdateWarehouseAsync(string id, ImportBillModel warehouseModel, string? username);
        public Task DeleteWarehouseAsync(string id);
    }
}