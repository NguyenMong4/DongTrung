using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using WebDongTrung.Common.Keycloak.Models;

namespace WebDongTrung.Common.Keycloak
{
    public class KeycloakServiceAccount : AuthenticationAccount
    {
        public string TokenUrl;
        public string client_id;
        public string client_secret;
        public AuthenticationResponse? authToken;
        private readonly HttpClient client = new();
        public KeycloakServiceAccount(string url, string realms, string client_id, string client_secret)
        {
            TokenUrl = url + $"/realms/{realms}/protocol/openid-connect/token";
            if (string.IsNullOrEmpty(client_secret))
            {
                throw new ArgumentNullException(nameof(client_secret));
            }
            this.client_id = client_id;
            this.client_secret = client_secret;
        }
        public override async Task<string?> GetToken(bool force = false)
        {
            try
            {
                if (authToken == null || force || IsExpired(authToken))
                {
                    FormUrlEncodedContent body;
                    if (string.IsNullOrEmpty(authToken?.refresh_token) || force)
                    {
                        var newTokenRequest = new Dictionary<string, string>(){
                            {"grant_type","client_credentials"},
                            {"client_id",client_id},
                            {"client_secret", client_secret}
                        };
                        body = new FormUrlEncodedContent(newTokenRequest);
                    }
                    else
                    {
                        var refreshTokenRequest = new Dictionary<string, string>(){
                            {"client_id", client_id},
                            {"client_secret", client_secret },
                            {"grant_type", "refresh_token"},
                            {"refresh_token", authToken.refresh_token}
                        };
                        body = new FormUrlEncodedContent(refreshTokenRequest);
                    }
                    var response = await client.PostAsync(TokenUrl, body);
                    var responseMsg = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        var response_body = await response.Content.ReadAsStringAsync();
                        if (string.IsNullOrEmpty(response_body))
                        {
                            return null;
                        }
                        authToken = JsonSerializer.Deserialize<AuthenticationResponse>(response_body);
                        return CreateJWTToken();
                    }
                }
                else
                {
                    return CreateJWTToken();
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
        private string? CreateJWTToken()
        {
            if (authToken != null)
            {
                return string.Join(" ", authToken.token_type, authToken.access_token);
            }
            return null;
        }
    }
}