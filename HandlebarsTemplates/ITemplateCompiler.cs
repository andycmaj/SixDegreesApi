using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HandlebarsTemplates
{
    public interface ITemplateCompiler
    {
        string Compile();

        TemplateCompiler IncludeTemplateFiles(params string[] templateFileNames);
    }
}
