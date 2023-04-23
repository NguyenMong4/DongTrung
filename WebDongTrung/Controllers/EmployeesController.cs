using Microsoft.AspNetCore.Mvc;
using WebDongTrung.Models;
using WebDongTrung.Repositories;

namespace WebDongTrung.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployees _employee;

        public EmployeesController(IEmployees employee)
        {
            _employee = employee;
        }
        [HttpPost("Login")]
        public IActionResult CheckLogin(LoginModel loginModel)
        {

            var userName =  _employee.CheckLogin(loginModel);
            if (!string.IsNullOrEmpty(userName))
            {
                Response.Cookies.Append("CookieUserName", userName, new CookieOptions()
                {
                    HttpOnly = true,
                    SameSite = SameSiteMode.None,
                    Secure = false,
                });
                return Ok();
            }
            
           return NotFound();

        }
        // [HttpGet]
        // public async Task<IActionResult> GetAllEmployee()
        // {
        //     try
        //     {
        //         return Ok(await _employee.GetAllEmployeeAsync());
        //     }
        //     catch
        //     {
        //         return BadRequest();
        //     }
        // }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployee(string id)
        {
            var employee = await _employee.GetEmployeeAsync(id);
            return employee == null ? NotFound() : Ok(employee);
        }

        // [HttpPost]
        // public async Task<ActionResult> AddEmployee(EmployeeModel employee)
        // {
        //     try
        //     {
        //         var newEmp = await _employee.AddEmployeeAsync(employee);
        //         var emp = await _employee.GetEmployeeAsync(newEmp!);
        //         return emp == null ? NotFound() : Ok(emp);
        //     }
        //     catch
        //     {
        //         return BadRequest();
        //     }
        // }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(string id, EmployeeModel employee)
        {
            try
            {
                await _employee.UpdateEmployeeAsync(id, employee);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(string id)
        {
            try
            {
                await _employee.DeleteEmployeeAsync(id);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }

        }
    }
}