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

        public async Task<string> AddWarehouseAsync(WarehouseModel warehouse)
        {
            var id = "Imp" + DateTime.Now.ToString("yyyyMMddHHmm");
            // foreach (var item in warehouse.IdProductLst)
            // {
            //     warehouse.IdProduct = item;
            //     var product = _contex.Products.SingleOrDefault(p => p.Id == item);
            //     if (product != null)
            //     {
            //         product.RealityQuantity += item.ImportQuantity;
            //         item.RealityQuantity = product.RealityQuantity;
            //         item.SystemQuantity = product.SystemQuantity;
            //         _contex.Products.Update(product);
            //     }
            //     _contex.Warehouses.AddAsync(item);
            //     await _contex.SaveChangesAsync();
            // }
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

        public async Task<IEnumerable<Warehouse>> GetWarehouseAsync(string id)
        {
            var warehouse = await _contex.Warehouses!.Select(w=>w.Id ==id).ToListAsync();
            return _mapper.Map<IEnumerable<Warehouse>>(warehouse);
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