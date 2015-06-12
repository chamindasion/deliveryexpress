using AP.PD.Business.Domain;
using AP.PD.Business.Interface;
using AP.PD.CLP.Web.AuthenticationServer.API;
using AP.PD.Shared.DI;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Practices.Unity;
using Owin;
using System;
using System.Web.Http;

[assembly: OwinStartup(typeof(AP.PD.Web.AuthenticationServer.API.Startup))]
namespace AP.PD.Web.AuthenticationServer.API
{

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            var container = new UnityContainer();
            container.RegisterType<IAuthService, AuthService>(new HierarchicalLifetimeManager());
            config.DependencyResolver = new UnityResolver(container);
            //var cors = new EnableCorsAttribute("*", "*", "*");
            //config.EnableCors(cors);
            ConfigureOAuth(app);
            WebApiConfig.Register(config);
            //Allow CORS for ASP.NET Web API
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(config);
        }

        public void ConfigureOAuth(IAppBuilder app)
        {
            var oAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                //TODO: Change and check for Https
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                //Setting token expiration
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                //Override OAuthAuthorizationServerProvider Implementation for custom 
                //ClpAuthorizationServerProvider class for user validation
                Provider = new ClpAuthorizationServerProvider()
            };
            //Token Generation
            //Passing oAuthServerOptions so that we are adding the authentication middleware to the pipeline
            app.UseOAuthAuthorizationServer(oAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}