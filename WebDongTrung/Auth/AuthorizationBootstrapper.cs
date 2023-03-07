using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebDongTrung.Auth
{
    public abstract class AuthorizationBootstrapper
    {
         public abstract Task BootstrapClient();
        public abstract Task BootstrapRoles();
    }
}