using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebDongTrung.Datas;
using WebDongTrung.DTO;
using WebDongTrung.Models;

namespace WebDongTrung.Repositories
{
    public interface IEmployees
    {
       public Task<IEnumerable<Employee>> GetAllEmployeeAsync();
       public Task<Employee?> GetEmployeeAsync(string id);
       public Task<string?> AddEmployeeAsync(EmployeeModel employee);
       public Task UpdateEmployeeAsync(string username, EmployeeModel employee);
       public Task DeleteEmployeeAsync(string username);
    }
}