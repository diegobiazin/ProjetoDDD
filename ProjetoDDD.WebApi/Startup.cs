using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using System.Web.Http;
using System.Net.Http.Formatting;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.Linq;
using AutoMapper;
using System.Web.Profile;
using ProjetoDDD.Services;
using ProjetoDDD.WebApi.Resolvers;
using ProjetoDDD.IoC;
using ProjetoDDD.WebApi.Filters;
using FluentValidation.WebApi;
using Microsoft.Owin.Security.OAuth;
using ProjetoDDD.WebApi.Provider;
using Microsoft.Owin.Cors;
using System.Web.Http.Cors;
using ProjetoDDD.Services.Interfaces;

[assembly: OwinStartup(typeof(ProjetoDDD.WebApi.Startup))]

namespace ProjetoDDD.WebApi
{
    public class Startup
    {
        public static HttpConfiguration HttpConfiguration { get; private set; }


        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration = new HttpConfiguration();
            HttpConfiguration.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;
            HttpConfiguration.Formatters.Remove(HttpConfiguration.Formatters.XmlFormatter);
            HttpConfiguration.Formatters.Add(HttpConfiguration.Formatters.JsonFormatter);

            var formatters = HttpConfiguration.Formatters.OfType<JsonMediaTypeFormatter>().First();
            var jsonSettings = HttpConfiguration.Formatters.JsonFormatter.SerializerSettings;
            jsonSettings.Formatting = Formatting.Indented;
            jsonSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            formatters.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Local;
            HttpConfiguration.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

            HttpConfiguration.Formatters.JsonFormatter.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.None;

            HttpConfiguration.MapHttpAttributeRoutes();

            HttpConfiguration.Routes.MapHttpRoute(
                name: "DefaultRoute",
                routeTemplate: "api/{controller}/{id}",
                defaults: new
                {
                    id = RouteParameter.Optional
                }
            );

            FluentValidationModelValidatorProvider.Configure(HttpConfiguration);

            HttpConfiguration.DependencyResolver = new UnityResolver(UnityContainerLoader.Load());
            HttpConfiguration.Filters.Add(new ValidateModelStateFilter());

            Mapper.Initialize(x => x.AddProfile(new MapperDefaultProfile()));
            ConfigureOAuth(app);
            app.UseCors(CorsOptions.AllowAll);
            app.UseWebApi(HttpConfiguration);
        }

        public void ConfigureOAuth(IAppBuilder app)
        {
            
            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/Token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new ApplicationOAuthProvider(HttpConfiguration.DependencyResolver.GetService(typeof(IProdutoService)) as IProdutoService)
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

        }
    }
}
