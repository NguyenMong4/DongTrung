using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using WebDongTrung.Common.Keycloak.Models;

namespace WebDongTrung.Common.Keycloak
{
    public abstract class AuthenticationAccount
    {
        public abstract Task<string?> GetToken(bool force = false);
        protected bool IsExpired(AuthenticationResponse? authToken)
        {
            try
            {
                if (authToken == null || string.IsNullOrEmpty(authToken?.AccessToken)) 
                {
                    return true;
                }
                var jwtToken = new JwtSecurityToken(authToken.AccessToken);
                if (jwtToken == null)
                {
                    return true;
                }
                if (jwtToken.ValidTo < DateTime.UtcNow.Add(TimeSpan.FromMinutes(5)))
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return true;
            }
        }
    }
}