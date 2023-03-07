using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebDongTrung.Datas;
using WebDongTrung.DTO;
using WebDongTrung.Models;

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

        public async Task<string?> AddEmployeeAsync(EmployeeModel employee)
        {
            var emp = _mapper.Map<Employee>(employee);
            await _contex.Employees!.AddAsync(emp);
            await _contex.SaveChangesAsync();
            return employee.Id;
        }

        public async Task DeleteEmployeeAsync(string username)
        {
            var employee = _contex.Employees!.SingleOrDefault(e => e.UserName == username);
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

        public async Task UpdateEmployeeAsync(string username, EmployeeModel employee)
        {
            if (username.Equals(employee.UserName))
            {
                var emp = _mapper.Map<Employee>(employee);
                _contex.Employees!.Update(emp);
                await _contex.SaveChangesAsync();
            }
        }
    }
}