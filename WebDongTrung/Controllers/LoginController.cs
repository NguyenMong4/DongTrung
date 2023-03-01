using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebDongTrung.Models;

namespace WebDongTrung.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        [HttpPost]
        public async Task<JsonResult> Login([FromBody] LoginModel loginModel)
        {

            HttpClient clien = new();
            var content = new FormUrlEncodedContent(new[]{
                new KeyValuePair<string, string>("client_id", "shop-dongtrung"),
                new KeyValuePair<string, string>("client_secret", "Zg8U8HGu4bhUAus4LSRGzmksbB71MUZL"),
                new KeyValuePair<string, string>("username", loginModel.Username),
                new KeyValuePair<string, string>("password", loginModel.Password),
                new KeyValuePair<string, string>("grant_type", "password")
            });
            var respon = await clien.PostAsync("http://localhost:8080/realms/master/protocol/openid-connect/token", content);
            return new JsonResult(JsonSerializer.Deserialize<object>(await respon.Content.ReadAsStringAsync()));
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateAccount([FromBody] LoginModel loginModel)
        {
            var token =
            "eyJhbGciOiJSUzI1NiIsInR5cCIgOiAiSldUIiwia2lkIiA6ICI1T0JhdVNRMVN5bWNYV2ZxTmo5bndXMTIxeTl0OF9tMlRJaDU2N0d3R1JvIn0.eyJleHAiOjE2Nzc2NjE3MDgsImlhdCI6MTY3NzY2MTY0OCwianRpIjoiMzQyOWY1NWEtOGIzMC00MjYyLWE3YTgtMjBhMTEwMmFjZGI2IiwiaXNzIjoiaHR0cDovL2xvY2FsaG9zdDo4MDgwL3JlYWxtcy9tYXN0ZXIiLCJhdWQiOlsibWFzdGVyLXJlYWxtIiwiYWNjb3VudCJdLCJzdWIiOiI4Nzg4YWVhZi1jNzRhLTQ5OTEtYjJjMi0yNDZhOTI2YzVkMzAiLCJ0eXAiOiJCZWFyZXIiLCJhenAiOiJzaG9wLWRvbmd0cnVuZyIsInNlc3Npb25fc3RhdGUiOiI3MWI4Y2YzZC1jMDIwLTQ5ZTgtYWZkZS02OGI1ODRjYzVjM2YiLCJhY3IiOiIxIiwiYWxsb3dlZC1vcmlnaW5zIjpbIi8qIl0sInJlYWxtX2FjY2VzcyI6eyJyb2xlcyI6WyJjcmVhdGUtcmVhbG0iLCJkZWZhdWx0LXJvbGVzLW1hc3RlciIsIm9mZmxpbmVfYWNjZXNzIiwiYWRtaW4iLCJ1bWFfYXV0aG9yaXphdGlvbiJdfSwicmVzb3VyY2VfYWNjZXNzIjp7Im1hc3Rlci1yZWFsbSI6eyJyb2xlcyI6WyJ2aWV3LXJlYWxtIiwidmlldy1pZGVudGl0eS1wcm92aWRlcnMiLCJtYW5hZ2UtaWRlbnRpdHktcHJvdmlkZXJzIiwiaW1wZXJzb25hdGlvbiIsImNyZWF0ZS1jbGllbnQiLCJtYW5hZ2UtdXNlcnMiLCJxdWVyeS1yZWFsbXMiLCJ2aWV3LWF1dGhvcml6YXRpb24iLCJxdWVyeS1jbGllbnRzIiwicXVlcnktdXNlcnMiLCJtYW5hZ2UtZXZlbnRzIiwibWFuYWdlLXJlYWxtIiwidmlldy1ldmVudHMiLCJ2aWV3LXVzZXJzIiwidmlldy1jbGllbnRzIiwibWFuYWdlLWF1dGhvcml6YXRpb24iLCJtYW5hZ2UtY2xpZW50cyIsInF1ZXJ5LWdyb3VwcyJdfSwiYWNjb3VudCI6eyJyb2xlcyI6WyJtYW5hZ2UtYWNjb3VudCIsIm1hbmFnZS1hY2NvdW50LWxpbmtzIiwidmlldy1wcm9maWxlIl19fSwic2NvcGUiOiJwcm9maWxlIGVtYWlsIiwic2lkIjoiNzFiOGNmM2QtYzAyMC00OWU4LWFmZGUtNjhiNTg0Y2M1YzNmIiwiZW1haWxfdmVyaWZpZWQiOmZhbHNlLCJwcmVmZXJyZWRfdXNlcm5hbWUiOiJhZG1pbiIsImdpdmVuX25hbWUiOiIiLCJmYW1pbHlfbmFtZSI6IiIsImVtYWlsIjoibmRjcGhhbUBnbWFpbC5jb20ifQ.GUIavH9hj1C4963VTwYWY_AoSPwNldnOd7DOt93NuVUZTu-DVSP-4cyyNelMP0f0nmmivP2vL0klQ3mxpUdL3ltw1le8c74_-Q5UhCpqu1SjPcpZtCnGnsdEJHqsurFKVKgkdwXqL7wc8Saa7_0BAXlRho1leUjI6MqyVBAx8IS-iQv9LrRu2CxEO3uISVd8Sy-5bGVqHCrb5UHiKwlh4FSxwbFAaA8CTQXDIaU4HZhDR97Q3ubY-TMmoD03zad_Sk2rm37iq-ywZqEhWO-ydJ31j0QQgQfWuynIlkwMOKiD0xqkIGc-R9UvM5i1mZ8bdEtxOxo0y5R5qd9iJq3uWQ";
            HttpClient client = new();
            HttpRequestMessage requestMessage = new()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("http://localhost:8080/admin/realms/master/users")
            };
            requestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            requestMessage.Content = new StringContent(JsonSerializer.Serialize(new
            {
                username = loginModel.Username,
                credentials = new List<object>{
                    new {
                        type = "password",
                        value = loginModel.Password
                    }
                }
            }));

            var respone = await client.SendAsync(requestMessage);
            if (respone.IsSuccessStatusCode)
            {
                return Ok();
            }
            else
            {
                return StatusCode(500, await respone.Content.ReadAsStreamAsync());
            }
        }
    }
}