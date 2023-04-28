using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebDongTrung.Datas;
using WebDongTrung.DTO.WareHouseDto;
using WebDongTrung.Models;

namespace WebDongTrung.Repositories;

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
            int? totalPrice = 0;
            string id = "Imp" + DateTime.Now.ToString("yyyyMMddHHmm");
            var impBill = _mapper.Map<ImportBill>(newWarehouse);
            impBill.Id = id;
            impBill.CreateAt = DateTime.Now;
            impBill.CreateId = username;

            foreach (var item in newWarehouse.ProductWarehouses)
            {
                Warehouse wh = new()
                {
                    BillId = id,
                    ProductId = item.ProductId,
                    ImportPrice = item.ImportPrice,
                    ImportQuantity = item.ImportQuantity,
                };
                totalPrice += item.ImportPrice;
                _contex.Warehouses!.Add(wh);
            }
            impBill.Total_price = totalPrice;
            _contex.ImportBills!.Add(impBill);
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
        var warehouse = _contex.Warehouses!.Where(e => e.BillId == id);
        var bill = _contex.ImportBills!.SingleOrDefault(e => e.Id == id);
        if (warehouse != null && bill != null)
        {
            _contex.Warehouses!.RemoveRange(warehouse);
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

    public async Task<ImportBillModel?> GetWarehouseAsync(string id)
    {
        var warehouse = await _contex.ImportBills!.AsNoTracking()
            .Where(x => x.Id == id)
            .Include(x => x.Warehouses).ThenInclude(x => x.Product)
            .ToListAsync();
        if (warehouse.Count == 0) { return null; }
        return new ImportBillModel
        {
            Id = warehouse[0].Id,
            Import_date = warehouse[0].Import_date,
            Total_price = warehouse[0].Total_price,
            ProductWarehouses = warehouse[0].Warehouses!.Select(x => new ProductWarehouseModel
            {
                ProductId = x.ProductId,
                NameProduct = x.Product!.Name,
                ImportPrice = x.ImportPrice,
                ImportQuantity = x.ImportQuantity
            }).ToList()
        };
    }

    public async Task UpdateWarehouseAsync(string id, ImportBillModel warehouseModel, string? username)
    {
        var warehouseDelete = _contex.Warehouses!.Where(e => e.BillId == id);
        if (warehouseDelete != null)
        {
            _contex.Warehouses!.RemoveRange(warehouseDelete);
            await _contex.SaveChangesAsync();
        }
        warehouseModel.Id = id;
        int? totalPrice = 0;
        var impBill = _mapper.Map<ImportBill>(warehouseModel);
        impBill.UpdateAt = DateTime.Now;
        impBill.UpdateId = username;

        foreach (var item in warehouseModel.ProductWarehouses)
        {
            var warehouse = _mapper.Map<Warehouse>(item);
            warehouse.BillId = id;
            totalPrice += warehouse.ImportPrice;
            await _contex.Warehouses!.AddAsync(warehouse);
        }
        impBill.Total_price = totalPrice;
        _contex.ImportBills!.Update(impBill);
        await _contex.SaveChangesAsync();
    }
}