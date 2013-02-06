#region Imports

using System.IO;
using System.Linq;
using System.Web;
using System.Web.Optimization;

using Ember.Handlebars;

using Microsoft.Ajax.Utilities;

#endregion

namespace SixDegrees.Web.Configuration.Bundles
{
    public class HandlebarsTemplateTransform : IBundleTransform
    {
        private readonly ITemplateCompiler _compiler;

        public HandlebarsTemplateTransform(ITemplateCompiler compiler)
        {
            _compiler = compiler;
        }

        public void Process(BundleContext context, BundleResponse response)
        {
            var files = response.Files.Select(fileInfo => fileInfo.FullName);
            string templateString = _compiler
                .IncludeTemplateFiles(files.ToArray())
                .Compile();

            response.ContentType = "text/javascript";
            response.Cacheability = HttpCacheability.Public;
            response.Content = templateString;
        }
    }
}