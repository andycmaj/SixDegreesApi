using System.Linq;

namespace SixDegrees.Web.Configuration.Bundles
{
    public interface ITemplateCompiler
    {
        string Compile();

        TemplateCompiler IncludeTemplateFiles(params string[] templateFileNames);
    }
}
