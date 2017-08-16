using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using System.Web.Http;

[assembly: OwinStartup(typeof(Altran.ILS.WebApi.Startup))]

namespace Altran.ILS.WebApi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration httpConfig = new HttpConfiguration();
            UnityConfig.RegisterComponents(httpConfig);
            ConfigureAuth(app); //In App_Start ->Startup.Auth
            WebApiConfig.Register(httpConfig);
            app.UseWebApi(httpConfig);
        }
    }
}
