using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebDongTrung.Common.Keycloak
{
    public class KeycloakAuthenticationService : IAuthenticationService
    {
        public async Task<string?> GetServiceAccountToken(AuthenticationAccount account)
        {
            return await account.GetToken();
        }
    }
}