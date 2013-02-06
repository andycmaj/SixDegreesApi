using System.Web.Optimization;

using Ninject.Modules;
using RestSharp;
using RestSharp.Deserializers;
using RestSharp.Serializers;
using SixDegrees.Data;
using SixDegrees.Data.Tmdb;
using SixDegrees.Web.Configuration.ActionResults;
using SixDegrees.Web.Configuration.Bundles;

namespace SixDegrees.Web.Configuration.Dependencies
{
    public class DependencyConfig : NinjectModule
    { 
        public override void Load()
        {
            Bind<BundleCollection>()
                  .ToConstant(BundleTable.Bundles);

            Bind<ITemplateCompiler>()
                .ToProvider<HandlebarsCompilerProvider>()
                .InSingletonScope();

            Bind<HandlebarsTemplateTransform>()
                .ToSelf()
                .InSingletonScope();

            Bind<IDeserializer>()
                .ToProvider<JsonSerializerProvider>()
                .InSingletonScope();

            Bind<ISerializer>()
                .ToProvider<JsonSerializerProvider>()
                .InSingletonScope();

            Bind<RestClientProvider>()
                .ToSelf()
                .InSingletonScope()
                .WithConstructorArgument("shouldUseFiddler", false);

            Bind<IRestClient>()
                .ToProvider<RestClientProvider>()
                .InSingletonScope();

            Bind<IDegreeRepository>()
                .To<TmdbDegreeRepository>()
                .InSingletonScope();

            Bind<ConfigurableJsonResult>()
                .ToProvider<ConfigurableJsonResultFactory>()
                .InTransientScope();
        }
    }
}