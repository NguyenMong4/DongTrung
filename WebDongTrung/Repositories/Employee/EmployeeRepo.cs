using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebDongTrung.Datas;

namespace WebDongTrung.Repositories
{
    public class EmployeeRepo : IEmployees
    {
        private readonly StoreDbContex _contex;
        private readonly IMapper _mapper;

        public EmployeeRepo(StoreDbContex contex, IMapper mapper)
        {
            _contex = contex;
            _mapper = mapper;
        }

        public async Task<string?> AddEmployeeAsync(Employee employee)
        {
            _contex.Employees!.AddAsync(employee);
            await _contex.SaveChangesAsync();
            return employee.Id;
        }

        public async Task DeleteEmployeeAsync(string id)
        {
            var employee = _contex.Employees!.SingleOrDefault(e => e.Id == id);
            if (employee != null)
            {
                _contex.Employees!.Remove(employee);
                await _contex.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeeAsync()
        {
            var employee = await _contex.Employees!.ToListAsync();
            return _mapper.Map<IEnumerable<Employee>>(employee);
        }

        public async Task<Employee?> GetEmployeeAsync(string id)
        {
            return await _contex.Employees!.FindAsync(id);
        }

        public async Task UpdateEmployeeAsync(string id, Employee employee)
        {
            if(id.Equals(employee.Id)){
                 _contex.Employees!.Update(employee);
                 await _contex.SaveChangesAsync();
            }
        }
    }
}