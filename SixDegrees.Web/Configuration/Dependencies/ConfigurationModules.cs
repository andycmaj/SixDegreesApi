﻿using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Dependencies;

using Ninject.Modules;

namespace SixDegrees.Web.Configuration.Dependencies
{
    public class ConfigurationModules : NinjectModule
    {
        public override void Load()
        {
            Bind<IDependencyResolver>()
                .To<NinjectApiDependencyResolver>();

            Bind<HttpConfiguration>()
                .ToConstant(GlobalConfiguration.Configuration);

            // TODO: JsonFormatterProvider to configure the JsonNetFormatter
            Bind<MediaTypeFormatter>()
                .To<JsonNetFormatter>();

            Bind<IWebConfigurationModule>()
                .To<WebApiConfigModule>();

            Bind<IWebConfigurationModule>()
                .To<MvcConfigModule>();
        }
    }
}