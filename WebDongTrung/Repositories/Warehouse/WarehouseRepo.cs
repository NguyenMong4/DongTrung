using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public WarehouseRepo(StoreDbContex contex, IMapper mapper)
        {
            _contex = contex;
            _mapper = mapper;
        }

        public async Task<string> AddWarehouseAsync(WarehouseModel warehouseModel)
        {
            string id = "Imp" + DateTime.Now.ToString("yyyyMMddHHmm");
            foreach (var item in warehouseModel.ProductWarehouses)
            {
                var product = _contex.Products!.SingleOrDefault(p => p.Id == item.IdProduct);
                if (product != null)
                {
                    Warehouse wh = new()
                    {
                        Id = id,
                        CreateAt = DateTime.Now,
                        CreateId = "nguyenpv",
                        UpdateAt = DateTime.Now,
                        UpdateId = "nguyenpv",
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
                    await _contex.SaveChangesAsync();
                }
            }

            return id;
        }

        public async Task DeleteWarehouseAsync(string id)
        {
            var warehouse = _contex.Warehouses!.SingleOrDefault(e => e.Id == id);
            if (warehouse != null)
            {
                _contex.Warehouses!.Remove(warehouse);
                await _contex.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Warehouse>> GetAllWarehouseAsync()
        {
            var warehouse = await _contex.Warehouses!.ToListAsync();
            return _mapper.Map<IEnumerable<Warehouse>>(warehouse);
        }

        public async Task<List<Warehouse>> GetWarehouseAsync(string id)
        {
            var warehouse = await _contex.Warehouses!.Where(w => w.Id.Contains(id)).ToListAsync();
            return _mapper.Map<List<Warehouse>>(warehouse);
        }

        public async Task UpdateWarehouseAsync(string id, Warehouse warehouse)
        {
            if (id.Equals(warehouse.Id))
            {
                _contex.Warehouses!.Update(warehouse);
                await _contex.SaveChangesAsync();
            }
        }
    }
}