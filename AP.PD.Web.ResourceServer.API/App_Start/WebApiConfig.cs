using AP.PD.Web.ExceptionHandling.API.Filters;
using AP.PD.Web.ExceptionHandling.API.Global;
using Newtonsoft.Json.Serialization;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.ExceptionHandling;

namespace AP.PD.Web.ResourceServer.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services            
            config.Filters.Add(new NotImplExceptionFilterAttribute());
            config.Services.Add(typeof(IExceptionLogger), new ClpExceptionLogger());
            config.Services.Replace(typeof(IExceptionHandler), new ClpExceptionHandler());

            //config.DependencyResolver = new NinjectResolver(NinjectConfig.CreateKernel());

            //Enable cors
            //TODO: Customize to allow only requested urls
            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

        }
    }
}
