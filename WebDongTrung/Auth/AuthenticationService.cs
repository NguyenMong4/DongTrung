using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using WebDongTrung.Common.Utils;

namespace WebDongTrung.Auth
{
    public static class AuthenticationService
    {
       public static IServiceCollection AddAuthenticationService(this IServiceCollection services)
        {
            var bootstrapper = new KeycloakAuthorizationBootstrapper();
            bootstrapper.BootstrapClient().RunSynchronously();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                var authUrl = Appsettings.GetConfig("ServiceAccount:auth-server-url");
                var realm = Appsettings.GetConfig("ServiceAccount:realm");

#warning https enable when go production
                options.RequireHttpsMetadata = false;
                options.MetadataAddress = $"{authUrl}realms/{realm}/.well-known/openid-configuration";
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateLifetime = true,
#warning validate audience (website) when go production
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true
                };
                //options.Events = new JwtBearerEvents
                //{
                //    OnAuthenticationFailed = c =>
                //    {
                //        return Task.CompletedTask;
                //    }
                //};
            });
            return services;
        }
    }
}