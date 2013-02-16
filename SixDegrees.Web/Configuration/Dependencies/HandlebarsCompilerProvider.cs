#region Imports

using System.Linq;
using System.Web;
using System.Web.Optimization;

using Ninject.Activation;

using SixDegrees.Web.Configuration.Bundles;

#endregion

namespace SixDegrees.Web.Configuration.Dependencies
{
    public class HandlebarsCompilerProvider : Provider<ITemplateCompiler>
    {
        private const string WorkingDirectory = "~/node_modules/handlebars/";
        private const string NodeExecutableFileName = "~/node_modules/node.exe";
        private const string OutputFileName = "~/Scripts/compiled_templates.js";

        protected override ITemplateCompiler CreateInstance(IContext context)
        {
            HttpServerUtility server = HttpContext.Current.Server;

            string workingDir = server.MapPath(WorkingDirectory);
            string nodeFileName = server.MapPath(NodeExecutableFileName);
            string outputFileName = server.MapPath(OutputFileName);

            TemplateCompiler compiler =
                new TemplateCompiler().WithWorkingDirectory(workingDir)
                                      .WithNodeJsExecutableFileName(nodeFileName)
                                      .WithOutputFileName(outputFileName)
                                      .WithMinification(BundleTable.EnableOptimizations)
                                      .WithAMDExport(false);
            return compiler;
        }
    }
}