using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Dependencies;

namespace SixDegrees.Web.Configuration
{
    public class WebApiConfigModule : IWebConfigurationModule
    {
        private readonly HttpConfiguration _apiConfig;
        private readonly IDependencyResolver _dependencyResolver;
        private readonly MediaTypeFormatter _jsonFormatter;

        public WebApiConfigModule(IDependencyResolver dependencyResolver,
                                  HttpConfiguration apiConfig,
                                  MediaTypeFormatter jsonFormatter)
        {
            _dependencyResolver = dependencyResolver;
            _apiConfig = apiConfig;
            _jsonFormatter = jsonFormatter;
        }

        public void Configure()
        {
            _apiConfig.DependencyResolver = _dependencyResolver;

            _apiConfig.Formatters[0] = _jsonFormatter;

            _apiConfig.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{type}/{id}",
                defaults: new
                {
                    type = RouteParameter.Optional,
                    id = RouteParameter.Optional
                }
            );
        }
    }
}