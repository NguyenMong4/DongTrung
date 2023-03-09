using System.Text.RegularExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebDongTrung.Models;
using WebDongTrung.Common.Keycloak;
using System.Text;
using NuGet.Protocol;
using WebDongTrung.Common.Keycloak.Models;
using WebDongTrung.DTO;
using WebDongTrung.Datas;
using WebDongTrung.Repositories;

namespace WebDongTrung.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        public IConfiguration _config = null!;
        public IEmployees _employee;
        public AccountController(IConfiguration config, IEmployees employee)
        {
            _config = config;
            _employee = employee;
        }
        public string url = "http://localhost:8080";
        [HttpPost]
        public async Task<JsonResult> Login([FromBody] LoginModel loginModel)
        {
            HttpClient clien = new();
            var content = new FormUrlEncodedContent(new[]{
                new KeyValuePair<string, string>("client_id", _config["KeyCloak:client_id"]),
                new KeyValuePair<string, string>("client_secret", _config["KeyCloak:client_secret"]),
                new KeyValuePair<string, string>("username", loginModel.Username),
                new KeyValuePair<string, string>("password", loginModel.Password),
                new KeyValuePair<string, string>("grant_type", "password")
            });
            var respon = await clien.PostAsync($"{url}/realms/{_config["KeyCloak:realms"]}/protocol/openid-connect/token", content);
            var token = new JsonResult(JsonSerializer.Deserialize<object>(await respon.Content.ReadAsStringAsync()));
            return token;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateAccount([FromBody] EmployeeModel employee)
        {
            var token = new KeycloakServiceAccount(url, _config["KeyCloak:realms"], _config["KeyCloak:client_id"], _config["KeyCloak:client_secret"]).GetToken().Result;
            HttpClient client = new();
            client.DefaultRequestHeaders.Remove("Accept");
            client.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "*/*");
            client.DefaultRequestHeaders.Remove("Authorization");
            client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", token);
            HttpRequestMessage requestMessage = new()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"{url}/admin/realms/{_config["KeyCloak:realms"]}/users"),
                Content = new StringContent(JsonSerializer.Serialize(new
                {
                    username = employee.UserName,
                    groups = new string[] { employee.Group },
                    email = employee.Email,
                    credentials = new List<object>{
                    new {
                        type = "password",
                        value = employee.PassWord,
                    }
                }
                }), Encoding.UTF8, "application/json")
            };

            var respone = await client.SendAsync(requestMessage);
            await respone.Content.ReadAsStringAsync();

            //create in database
            var newEmp = await _employee.AddEmployeeAsync(employee);
            var emp = await _employee.GetEmployeeAsync(newEmp!);

            if (respone.IsSuccessStatusCode && emp != null)
            {
                return Ok(emp);
            }
            else
            {
                return StatusCode(500, await respone.Content.ReadAsStreamAsync());
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var token = new KeycloakServiceAccount(url, _config["KeyCloak:realms"], _config["KeyCloak:client_id"], _config["KeyCloak:client_secret"]).GetToken().Result;
            HttpClient client = new();
            client.DefaultRequestHeaders.Remove("Accept");
            client.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "*/*");
            client.DefaultRequestHeaders.Remove("Authorization");
            client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", token);

            var respon = await client.GetAsync($"{url}/admin/realms/master/users");

            var users = JsonSerializer.Deserialize<List<UsersModel>>(await respon.Content.ReadAsStringAsync());
            foreach (var item in users!)
            {
                item.CreateAt = DateTimeOffset.FromUnixTimeMilliseconds(item.createdTimestamp);
            }

            return respon == null ? BadRequest() : Ok(users);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Getuser(string id)
        {
            var token = new KeycloakServiceAccount(url, _config["KeyCloak:realms"], _config["KeyCloak:client_id"], _config["KeyCloak:client_secret"]).GetToken().Result;
            HttpClient client = new();
            client.DefaultRequestHeaders.Remove("Accept");
            client.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "*/*");
            client.DefaultRequestHeaders.Remove("Authorization");
            client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", token);

            var respon = await client.GetAsync($"{url}/admin/realms/master/users/{id}");

            var user = JsonSerializer.Deserialize<UsersModel>(await respon.Content.ReadAsStringAsync());
            user!.CreateAt = DateTimeOffset.FromUnixTimeMilliseconds(user.createdTimestamp);

            return respon == null ? BadRequest() : Ok(user);
        }
        [HttpPut("{username}")]
        public async Task<IActionResult> UpdateUser(string username, EmployeeModel user)
        {
            var token = new KeycloakServiceAccount(url, _config["KeyCloak:realms"], _config["KeyCloak:client_id"], _config["KeyCloak:client_secret"]).GetToken().Result;
            HttpClient client = new();
            client.DefaultRequestHeaders.Remove("Accept");
            client.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "*/*");
            client.DefaultRequestHeaders.Remove("Authorization");
            client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", token);
            var content = new StringContent(JsonSerializer.Serialize(new UserUpdateDto
            {
                Email = user.Email,
                Groups = user.Group
            }), Encoding.UTF8, "application/json");

            var respone = await client.PutAsync($"{url}/admin/realms/master/users?username={username}", content);
            await _employee.UpdateEmployeeAsync(username, user);

            return respone == null ? BadRequest() : Ok(await respone.Content.ReadAsStringAsync());
        }
        [HttpDelete("{id},{username}")]
        public async Task<IActionResult> DeleteUser(string id, string username)
        {
            var token = new KeycloakServiceAccount(url, _config["KeyCloak:realms"], _config["KeyCloak:client_id"], _config["KeyCloak:client_secret"]).GetToken().Result;
            HttpClient client = new();
            client.DefaultRequestHeaders.Remove("Accept");
            client.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "*/*");
            client.DefaultRequestHeaders.Remove("Authorization");
            client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", token);
            await client.DeleteAsync($"{url}/admin/realms/master/users/{id}");
            await _employee.DeleteEmployeeAsync(username);
            return Ok();
        }
    }
}