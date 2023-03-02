using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebDongTrung.Common.Keycloak
{
    public interface IAuthenticationService
    {
        Task<string?> GetServiceAccountToken(AuthenticationAccount account);
    }
}