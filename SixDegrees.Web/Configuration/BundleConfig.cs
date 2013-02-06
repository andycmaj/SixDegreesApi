#region Imports

using System.Linq;
using System.Web.Optimization;

using BundleTransformer.Core.Transformers;
using BundleTransformer.Core.Translators;
using BundleTransformer.SassAndScss.Translators;

using SixDegrees.Web.Configuration.Bundles;

#endregion

namespace SixDegrees.Web.Configuration
{
    public static class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles, HandlebarsTemplateTransform handlebarsTemplateTransform)
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
                    "~/Scripts/libs/handlebars.runtime.js"));

            bundles.Add(
                new ScriptBundle("~/scripts/modules/search").Include(
                    "~/Scripts/CommonModule.js",
                    "~/Scripts/SearchModule.js"));
        }

        private static CssTransformer CreateCssTransformer()
        {
            ITranslator scssTranslator = new SassAndScssTranslator();

            CssTransformer transformer = new CssTransformer(new[] {scssTranslator});

            return transformer;
        }
    }
}