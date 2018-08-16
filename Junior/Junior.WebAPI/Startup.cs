using AutoMapper;
using Junior.SharedModels.DomainModels;
using Junior.SharedModels.DtoModels;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Owin;
using Swashbuckle.Application;
using System.IO;
using System.Net.Http.Formatting;
using System.Web.Http;

[assembly: OwinStartup(typeof(Junior.WebAPI.Startup))]

namespace Junior.WebAPI
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
            var config = new HttpConfiguration();

            // Return data as JSON
            config.Formatters.Clear();
            config.Formatters.Add(new JsonMediaTypeFormatter());
            config.Formatters.JsonFormatter.SerializerSettings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            // Configure API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = RouteParameter.Optional }
            );


            config.Routes.MapHttpRoute(
                name: "SwaggerRoot",
                routeTemplate: "",
                defaults: null,
                constraints: null,
                handler: new RedirectHandler((message => message.RequestUri.ToString()), "swagger"));


            //log4net.LogManager.GetLogger(GetType().Name).Info("BackendApi started.");

            // Swagger config
            config.EnableSwagger(c =>
            {
                c.SingleApiVersion("v1", "Junior.WebAPI");
                c.IncludeXmlComments(GetXmlCommentsPath());
            })
            .EnableSwaggerUi();

            //Automapper config
            Mapper.Initialize(mapper =>
            {
                mapper.CreateMap<Compound, CompoundDto>();
            });

            app.UseCors(CorsOptions.AllowAll);
            app.UseWebApi(config);
        }

        protected static string GetXmlCommentsPath()
        {
            return Path.Combine(System.Web.HttpRuntime.AppDomainAppPath, "bin", $"{typeof(Startup).Namespace}.xml");
        }
    }
}
