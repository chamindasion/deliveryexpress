using AP.PD.Business.Domain;
using AP.PD.Business.Interface;
using AP.PD.Shared.DI;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Practices.Unity;
using Owin;
using System.Web.Http;
using System.Web.Http.Cors;

[assembly: OwinStartup(typeof(AP.PD.Web.ResourceServer.API.Startup))]
namespace AP.PD.Web.ResourceServer.API
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            var container = new UnityContainer();
            container.RegisterType<ICategoryService, CategoryService>(new HierarchicalLifetimeManager());
            container.RegisterType<IDeliveryService, DeliveryService>(new HierarchicalLifetimeManager());
            config.DependencyResolver = new UnityResolver(container);
            ConfigureOAuth(app);
            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);
            WebApiConfig.Register(config);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(config);
        }

        private void ConfigureOAuth(IAppBuilder app)
        {
            //Token Consumption
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions
            {
            });
        }

    }
}