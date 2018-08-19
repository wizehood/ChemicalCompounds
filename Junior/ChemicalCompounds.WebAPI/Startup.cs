using AutoMapper;
using ChemicalCompounds.SharedModels.DomainModels;
using ChemicalCompounds.SharedModels.DtoModels;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Owin;
using Serilog;
using Swashbuckle.Application;
using System;
using System.IO;
using System.Net.Http.Formatting;
using System.Web.Http;

[assembly: OwinStartup(typeof(ChemicalCompounds.WebAPI.Startup))]

namespace ChemicalCompounds.WebAPI
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

            // Swagger config
            config.EnableSwagger(c =>
            {
                c.SingleApiVersion("v1", "ChemicalCompounds.WebAPI");
                c.IncludeXmlComments(GetXmlCommentsPath());
            })
            .EnableSwaggerUi();

            //Automapper config
            Mapper.Initialize(mapper =>
            {
                mapper.CreateMap<Compound, CompoundDto>();
            });

            //Serilog config
            var assemblyFolder = AppDomain.CurrentDomain.BaseDirectory;
            var log = new LoggerConfiguration()
               .WriteTo.RollingFile($@"{assemblyFolder}Logs\logs.txt",
                                   outputTemplate: "[{Timestamp:dd-MM-yyyy HH:mm:ss}] {Level:u3}: {Message:lj} {Exception} {NewLine}")
               .CreateLogger();
            Log.Logger = log;

            app.UseCors(CorsOptions.AllowAll);
            app.UseWebApi(config);

            Log.Information("Application started");
        }

        protected static string GetXmlCommentsPath()
        {
            return Path.Combine(System.Web.HttpRuntime.AppDomainAppPath, "bin", $"{typeof(Startup).Namespace}.xml");
        }
    }
}
