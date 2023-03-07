using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebDongTrung.Common.Utils
{
    public static class Appsettings
    {
        private static string clientId = string.Empty;
        public static string ClientId
        {
            get
            {
                if(string.IsNullOrEmpty(clientId))
                {
                    clientId = GetClientConfig("clientId");
                }
                return clientId;
            }
        }

        public static string GetConfig(string code)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
                .Build();
            return configuration[code];
        }

        public static string GetClientConfig(string code)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("Resources/client.json", optional: false, reloadOnChange: false)
                .Build();
            return configuration[code];
        }

        public static string GetClientConfigJson(string localPath)
        {
            return File.ReadAllText(localPath);
        }
    }
}