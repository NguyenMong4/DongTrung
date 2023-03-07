using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebDongTrung.Common.Keycloak;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using WebDongTrung.Common.Utils;
using System.Text.Json.Serialization;

namespace WebDongTrung.Auth
{
    public class KeycloakAuthorizationBootstrapper : AuthorizationBootstrapper
    {
         public override async Task BootstrapClient()
        {
            try
            {
                const string CONFIG_FILE = "Resources/client.json";

                var realmHost = Appsettings.GetConfig("ServiceAccount:auth-server-url");
                var realmName = Appsettings.GetConfig("ServiceAccount:realm");
                var adminClient = Appsettings.GetConfig("ServiceAccount:resource");
                var secret = Appsettings.GetConfig("ServiceAccount:credentials:secret");
                var serviceAccount = new KeycloakServiceAccount(realmHost, realmName, adminClient, secret);
                var accessToken = await serviceAccount.GetToken();

                var url = realmHost + "admin/realms/" + realmName + "/clients";
                var configJson = Appsettings.GetClientConfigJson(CONFIG_FILE);
                if (string.IsNullOrWhiteSpace(configJson))
                    throw new ArgumentNullException(nameof(configJson), "Keycloak client configuration file is empty.");
                var configJsonRootNode = JsonNode.Parse(configJson);
                if (configJsonRootNode == null)
                    throw new ArgumentNullException(nameof(configJson), "Keycloak client configuration file is empty.");
                var configJsonClientId = Appsettings.GetClientConfig("clientId");
                if (string.IsNullOrWhiteSpace(configJsonClientId))
                    throw new ArgumentNullException(nameof(configJsonClientId), "`clientId` does not exists in Keycloak client configuration file.");

                // create a client
                var http = new HttpClient();
                http.DefaultRequestHeaders.Remove("Accept");
                http.DefaultRequestHeaders.TryAddWithoutValidation("Accept", "*/*");
                http.DefaultRequestHeaders.Remove("Authorization");
                http.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", accessToken);
                var body = new StringContent(configJson, Encoding.UTF8, "application/json");
                var response = await http.PostAsync(url, body);
                if (response.IsSuccessStatusCode)
                {
                    response = await http.GetAsync(url);
                    var res_body = await response.Content.ReadAsStringAsync();
                    var clientInfo = JsonSerializer.Deserialize<IEnumerable<ClientRepresentation>>(res_body);
                    if (clientInfo == null)
                        throw new ArgumentNullException(nameof(clientInfo), "Keycloak server does not return client(s) info.");
                    var clientUUID = clientInfo?.Where(x => x.ClientId == configJsonClientId).FirstOrDefault()?.Id;
                    if (clientUUID == null)
                        throw new ArgumentNullException(nameof(clientUUID), "Keycloak server does not return created client.");
                    var added = configJsonRootNode?.AsObject().TryAdd("id", clientUUID);
                    if (added.HasValue && added.Value == false && configJsonRootNode != null)
                    {
                        configJsonRootNode.AsObject()["id"] = clientUUID;
                    }
                    using (var fs = File.Open(CONFIG_FILE, FileMode.Create))
                    {
                        var content = configJsonRootNode?.ToJsonString(new JsonSerializerOptions()
                        {
                            WriteIndented = true,
                            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                        });
                        if (string.IsNullOrWhiteSpace(content))
                            throw new ArgumentNullException(nameof(content), "Json configuration is empty.");
                        var byteArr = Encoding.UTF8.GetBytes(content);
                        fs.Write(byteArr);
                    }
                }
                else if (response.StatusCode == HttpStatusCode.Conflict)
                {
                    var clientUUID = Appsettings.GetClientConfig("id");
                    if (string.IsNullOrWhiteSpace(clientUUID))
                        throw new ArgumentNullException(nameof(clientUUID), "Keycloak Id of client was not found.");
                    url += $"/{clientUUID}";
                    response = await http.PutAsync(url, body);
                    if (!response.IsSuccessStatusCode)
                    {
                        Environment.Exit(-1);
                    }
                }
                else
                {
                    Environment.Exit(-1);
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Keycloak client configuration file was not found.");
                Environment.Exit(-1);
            }
            catch (ArgumentNullException)
            {
                Environment.Exit(-1);
            }
            catch (Exception)
            {
                Environment.Exit(-1);
            }
        }

        public override Task BootstrapRoles()
        {
            return Task.CompletedTask;
        }

        internal class ClientRepresentation
        {
            [JsonPropertyName("id")]
            public string? Id { get; set; }

            [JsonPropertyName("clientId")]
            public string? ClientId { get; set; }
        }
    }
}