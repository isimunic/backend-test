using Altran.ILS.Data.Interfaces;
using Altran.ILS.Data.Repositories;
using Altran.ILS.Services;
using Altran.ILS.WebApi.Controllers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler;
using Microsoft.Practices.Unity;
using System.Data.Entity;
using System.Web.Http;
using Unity.WebApi;

namespace Altran.ILS.WebApi
{
    public static class UnityConfig
    {
        public static void RegisterComponents(HttpConfiguration config)
        {
			var container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            container.RegisterType<IClientService, ClientService>(new HierarchicalLifetimeManager());
            container.RegisterType<IPolicyService, PolicyService>(new HierarchicalLifetimeManager());
            container.RegisterType<IClientRepository, ClientRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IPolicyRepository, PolicyRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<ISecureDataFormat<AuthenticationTicket>, SecureDataFormat<AuthenticationTicket>>();
            container.RegisterType<AccountController>(new InjectionConstructor());

            config.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}