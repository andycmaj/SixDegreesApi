#region Imports

using System.Linq;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

using BundleTransformer.Core.Transformers;
using BundleTransformer.Core.Translators;
using BundleTransformer.SassAndScss.Translators;

using SixDegrees.Data;
using SixDegrees.Web.Configuration.Binders;
using SixDegrees.Web.Configuration.Bundles;
using SixDegrees.Web.Configuration.Filters;

#endregion

namespace SixDegrees.Web.Configuration
{
    public class MvcConfigModule : IWebConfigurationModule
    {
        private const string TypeConstraint = "movie|person";

        private readonly BundleCollection _bundles;
        private readonly HandlebarsTemplateTransform _jsTemplateTransform;
        private readonly GlobalFilterCollection _globalFilters;
        private readonly ModelBinderDictionary _modelBinders;
        private readonly RouteCollection _routes;

        public MvcConfigModule(RouteCollection routes,
                               BundleCollection bundles,
                               HandlebarsTemplateTransform jsTemplateTransform,
                               ModelBinderDictionary modelBinders,
                               GlobalFilterCollection globalFilters)
        {
            _routes = routes;
            _bundles = bundles;
            _jsTemplateTransform = jsTemplateTransform;
            _modelBinders = modelBinders;
            _globalFilters = globalFilters;
        }

        public void Configure()
        {
            RegisterRoutes(_routes);
            RegisterGlobalFilters(_globalFilters);
            RegisterModelBinders(_modelBinders);
            RegisterBundles(_bundles, _jsTemplateTransform);
        }

        private static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.IgnoreRoute("Views/Templates/{file}.html");

            /**
             * JSON API
             */

            routes.MapRoute("Search",
                            "search/{type}/{query}",
                            new
                            {
                                controller = "Degree",
                                action = "Search"
                            },
                            new
                            {
                                type = TypeConstraint,
                                query = @"[\w\s]+"
                            });

            routes.MapRoute("Lookup",
                            "{type}/{id}",
                            new
                            {
                                controller = "Degree",
                                action = "Lookup"
                            },
                            new
                            {
                                type = TypeConstraint
                            });

            /**
             * Pages / Views
             */

            routes.MapRoute("Home",
                            "",
                            new
                            {
                                controller = "Home",
                                action = "Index"
                            });
        }

        private static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new AllowJsonGetFilterAttribute());
            filters.Add(new HandleErrorAttribute());
        }

        private static void RegisterModelBinders(ModelBinderDictionary binders)
        {
            binders.Add(typeof (DegreeType), new DegreeTypeModelBinder());
        }

        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles,
                                           HandlebarsTemplateTransform handlebarsTemplateTransform)
        {
            CssTransformer cssTransformer = CreateCssTransformer();
            Bundle mainStyles = new StyleBundle("~/styles/bundles/main").Include(
                "~/Content/normalize.css",
                "~/Content/main.scss");
            mainStyles.Transforms.Add(cssTransformer);
            bundles.Add(mainStyles);

            Bundle templates = new Bundle("~/templates").Include("~/Views/Templates/*.html");
            templates.Transforms.Add(handlebarsTemplateTransform);
            bundles.Add(templates);

            bundles.Add(
                new ScriptBundle("~/scripts/bundles/libs").Include(
                    "~/Scripts/libs/modernizr-{version}.js",
                    "~/Scripts/libs/jquery-{version}.js",
                    "~/Scripts/libs/underscore.js",
                    "~/Scripts/libs/backbone.js",
                    "~/Scripts/libs/backbone.marionette.js",
                    "~/Scripts/libs/handlebars.runtime.js"));

            bundles.Add(
                new ScriptBundle("~/scripts/modules/search").Include(
                    "~/Scripts/CommonModule.js",
                    "~/Scripts/SearchModule.js"));
        }

        private static CssTransformer CreateCssTransformer()
        {
            ITranslator scssTranslator = new SassAndScssTranslator();
            var transformer = new CssTransformer(new[] {scssTranslator});

            return transformer;
        }
    }
}