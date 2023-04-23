using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebDongTrung.Datas;
using WebDongTrung.DTO.WareHouseDto;
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

        public async Task<string> AddWarehouseAsync(CreateWareHouseDto newWarehouse, string? username)
        {
            try
            {
                string id = "Imp" + DateTime.Now.ToString("yyyyMMddHHmm");
                var impBill = _mapper.Map<ImportBill>(newWarehouse);
                impBill.Id = id;
                impBill.CreateAt = DateTime.Now;
                impBill.CreateId = username;
                _contex.ImportBills!.Add(impBill);
                foreach (var item in newWarehouse.ProductWarehouses)
                {
                    var product = _contex.Products!.SingleOrDefault(p => p.Id == item.IdProduct);
                    if (product != null)
                    {
                        Warehouse wh = new()
                        {
                            BillId = id,
                            ProductId = item.IdProduct,
                            ImportPrice = item.ImportPrice,
                            ImportQuantity = item.ImportQuantity,
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
            var warehouse = _contex.Warehouses!.SingleOrDefault(e => e.BillId == id);
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
            // var warehouse = _contex.ImportBills!
            //     .Join(_contex.Warehouses!, bill => bill.Id, wareh => wareh.IdBill, (bill, wareh) => new { Bill = bill, WareHouse = wareh })
            //     .Join(_contex.Products!, w => w.WareHouse.IdProduct, p => p.Id, (w, p) => new { Prod = p, Import = w })
            //     .Where(x => x.Import.Bill.Id.Contains(id)).ToList();
            var warehouse = await _contex.Warehouses!.AsNoTracking()
                .Where(x => x.BillId == id)
                .Include(x => x.Product).Include(x => x.Bill).ToListAsync();

            return _mapper.Map<WarehouseModel>(warehouse);
        }

        public async Task UpdateWarehouseAsync(string id, WarehouseModel warehouseModel, string? username)
        {
            warehouseModel.Id = id;
            var impBill = _mapper.Map<ImportBill>(warehouseModel);
            impBill.UpdateAt = DateTime.Now;
            impBill.UpdateId = username;
            var warehouse = _mapper.Map<Warehouse>(warehouseModel);
            _contex.ImportBills!.Update(impBill);
            _contex.Warehouses!.Update(warehouse);
            await _contex.SaveChangesAsync();
        }
    }
}