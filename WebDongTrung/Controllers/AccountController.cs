using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using WebDongTrung.Models;
using WebDongTrung.Common.Keycloak;
using System.Text;
using WebDongTrung.DTO;
using WebDongTrung.Repositories;
using WebDongTrung.Common.Keycloak.Models;
using System.IdentityModel.Tokens.Jwt;
using WebDongTrung.Common.Utils;

namespace WebDongTrung.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        public IConfiguration _config = null!;
        public IEmployees _employee;
        public AccountController(IConfiguration config, IEmployees employee )
        {
            _config = config;
            _employee = employee;
        }
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
            var respon = await clien.PostAsync($"{_config["KeyCloak:url"]}/realms/{_config["KeyCloak:realms"]}/protocol/openid-connect/token", content);
            var authenticationResponse = JsonSerializer.Deserialize<AuthenticationResponse>(await respon.Content.ReadAsStringAsync());
            var userName =  new JwtSecurityToken(authenticationResponse.access_token).Claims.FirstOrDefault(c => c.Type == "preferred_username").Value;
            Response.Cookies.Append("CookieAccestoken",authenticationResponse.access_token,new CookieOptions(){
                HttpOnly = true,
                SameSite = SameSiteMode.None,
                Secure = false,
            });
            Response.Cookies.Append("CookieUserName",userName,new CookieOptions(){
                HttpOnly = true,
                SameSite = SameSiteMode.None,
                Secure = false,
            });
            return new JsonResult(authenticationResponse);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateAccount([FromBody] EmployeeModel employee)
        {
            //var token = new KeycloakServiceAccount(_config["KeyCloak:url"], _config["KeyCloak:realms"], _config["KeyCloak:client_id"], _config["KeyCloak:client_secret"]).GetToken().Result;
            var token = Request.Cookies["CookieAccestoken"];
            HttpClient client = new();
            client.DefaultRequestHeaders.Remove("Accept");
            client.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "*/*");
            client.DefaultRequestHeaders.Remove("Authorization");
            client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", token);
            HttpRequestMessage requestMessage = new()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"{_config["KeyCloak:url"]}/admin/realms/{_config["KeyCloak:realms"]}/users"),
                Content = new StringContent(JsonSerializer.Serialize(new
                {
                    username = employee.UserName,
                    email = employee.Email,
                    enabled = true,
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

            var user = await client.GetAsync($"{_config["KeyCloak:url"]}/admin/realms/master/users?username={employee.UserName}");
            var userM = JsonSerializer.Deserialize<List<UsersModel>>(await user.Content.ReadAsStringAsync());
            if (userM?.Count > 0)
            {
                string id = userM[0].id;
                //create in database
                await _employee.AddEmployeeAsync(employee, id);
            }

            if (respone.IsSuccessStatusCode)
            {
                return Ok();
            }
            else
            {
                return StatusCode(500, await respone.Content.ReadAsStreamAsync());
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var token = new KeycloakServiceAccount(_config["KeyCloak:url"], _config["KeyCloak:realms"], _config["KeyCloak:client_id"], _config["KeyCloak:client_secret"]).GetToken().Result;
            HttpClient client = new();
            client.DefaultRequestHeaders.Remove("Accept");
            client.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "*/*");
            client.DefaultRequestHeaders.Remove("Authorization");
            client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", token);

            var respon = await client.GetAsync($"{_config["KeyCloak:url"]}/admin/realms/master/users");

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
            var token = new KeycloakServiceAccount(_config["KeyCloak:url"], _config["KeyCloak:realms"], _config["KeyCloak:client_id"], _config["KeyCloak:client_secret"]).GetToken().Result;
            HttpClient client = new();
            client.DefaultRequestHeaders.Remove("Accept");
            client.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "*/*");
            client.DefaultRequestHeaders.Remove("Authorization");
            client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", token);

            var respon = await client.GetAsync($"{_config["KeyCloak:url"]}/admin/realms/master/users/{id}");

            var user = JsonSerializer.Deserialize<UsersModel>(await respon.Content.ReadAsStringAsync());
            user!.CreateAt = DateTimeOffset.FromUnixTimeMilliseconds(user.createdTimestamp);

            return respon == null ? BadRequest() : Ok(user);
        }
        [HttpPut("{username}")]
        public async Task<IActionResult> UpdateUser(string username, EmployeeModel user)
        {
            var token = new KeycloakServiceAccount(_config["KeyCloak:url"], _config["KeyCloak:realms"], _config["KeyCloak:client_id"], _config["KeyCloak:client_secret"]).GetToken().Result;
            HttpClient client = new();
            client.DefaultRequestHeaders.Remove("Accept");
            client.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "*/*");
            client.DefaultRequestHeaders.Remove("Authorization");
            client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", token);
            var content = new StringContent(JsonSerializer.Serialize(new UserUpdateDto
            {
                Email = user.Email
            }), Encoding.UTF8, "application/json");
            var respone = await client.PutAsync($"{_config["KeyCloak:url"]}/admin/realms/master/users?username={username}", content);
            await _employee.UpdateEmployeeAsync(username, user);

            return respone == null ? BadRequest() : Ok(await respone.Content.ReadAsStringAsync());
        }
        [HttpDelete("{id},{username}")]
        public async Task<IActionResult> DeleteUser(string id, string username)
        {
            var token = new KeycloakServiceAccount(_config["KeyCloak:url"], _config["KeyCloak:realms"], _config["KeyCloak:client_id"], _config["KeyCloak:client_secret"]).GetToken().Result;
            HttpClient client = new();
            client.DefaultRequestHeaders.Remove("Accept");
            client.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "*/*");
            client.DefaultRequestHeaders.Remove("Authorization");
            client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", token);
            await client.DeleteAsync($"{_config["KeyCloak:url"]}/admin/realms/master/users/{id}");
            await _employee.DeleteEmployeeAsync(username);
            return Ok();
        }
    }
}