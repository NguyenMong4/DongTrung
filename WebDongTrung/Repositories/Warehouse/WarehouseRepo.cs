using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebDongTrung.Datas;
using WebDongTrung.Models;

namespace WebDongTrung.Repositories
{
    public class WarehouseRepo : IWarehouse
    {
        private readonly StoreDbContex _contex;
        private readonly IMapper _mapper;
        public static int PageSize { get; set; } = 10;
        public WarehouseRepo(StoreDbContex contex, IMapper mapper)
        {
            _contex = contex;
            _mapper = mapper;
        }

        public async Task<string> AddWarehouseAsync(WarehouseModel warehouseModel)
        {
            try
            {
                string id = "Imp" + DateTime.Now.ToString("yyyyMMddHHmm");
                var impBill = _mapper.Map<ImportBill>(warehouseModel);
                _contex.ImportBills!.Add(impBill);
                foreach (var item in warehouseModel.ProductWarehouses)
                {
                    var product = _contex.Products!.SingleOrDefault(p => p.Id == item.IdProduct);
                    if (product != null)
                    {
                        Warehouse wh = new()
                        {
                            IdBill = id,
                            IdProduct = item.IdProduct,
                            ImportPrice = item.ImportPrice,
                            ImportQuantity = item.ImportQuantity,
                            RealityQuantity = product.RealityQuantity,
                            SystemQuantity = product.SystemQuantity
                        };
                        product.RealityQuantity += item.ImportQuantity;
                        product.SystemQuantity += item.ImportQuantity;
                        _contex.Products!.Update(product);
                        _contex.Warehouses!.Add(wh);
                    }
                }
                await _contex.SaveChangesAsync();
                return id;
            }
            catch
            {
                throw;
            }
        }

        public async Task DeleteWarehouseAsync(string id)
        {
            var warehouse = _contex.Warehouses!.SingleOrDefault(e => e.IdBill == id);
            var bill = _contex.ImportBills!.SingleOrDefault(e => e.Id == id);
            if (warehouse != null && bill != null)
            {
                _contex.Warehouses!.Remove(warehouse);
                _contex.ImportBills!.Remove(bill);
                await _contex.SaveChangesAsync();
            }
        }

        public async Task<List<ImportBill>> GetAllImportBillAsync(int? page = 1)
        {
            var bills = _contex.ImportBills!.AsQueryable();
            if (page != null)
                bills = bills.Skip((int)((page - 1) * PageSize)).Take(PageSize);
            return await bills.OrderBy(x => x.CreateAt).ToListAsync();
        }

        public async Task<WarehouseModel> GetWarehouseAsync(string id)
        {
            var warehouse = await _contex.ImportBills!
                .Join(_contex.Warehouses!, bill => bill.Id, wareh => wareh.IdBill, (bill, wareh) => new { Bill = bill, WareHouse = wareh })
                .Where(x => x.Bill.Id.Contains(id)).OrderBy(p => p.WareHouse.IdProduct).FirstOrDefaultAsync();
            return _mapper.Map<WarehouseModel>(warehouse);
        }

        public async Task UpdateWarehouseAsync(string id, WarehouseModel warehouseModel)
        {
            if (id.Equals(warehouseModel.Id))
            {
                var impBill = _mapper.Map<ImportBill>(warehouseModel);
                var warehouse = _mapper.Map<Warehouse>(warehouseModel);
                _contex.ImportBills!.Update(impBill);
                _contex.Warehouses!.Update(warehouse);
                await _contex.SaveChangesAsync();
            }
        }
    }
}