using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebDongTrung.Datas;
using WebDongTrung.Models;

namespace WebDongTrung.Repositories
{
    public interface IEmployees
    {
       public Task<IEnumerable<Employee>> GetAllEmployeeAsync();
       public Task<Employee?> GetEmployeeAsync(string id);
       public Task<string?> AddEmployeeAsync(Employee employee);
       public Task UpdateEmployeeAsync(string id, Employee employee);
       public Task DeleteEmployeeAsync(string id);
    }
}