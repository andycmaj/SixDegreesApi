#region Imports

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

#endregion

namespace HandlebarsTemplates
{
    public class TemplateCompiler : ITemplateCompiler
    {
        /**
Precompile handlebar templates.
Usage: C:\Users\andycunn\Documents\GitHub\SixDegrees\SixDegrees.Web\bundler\node.exe C:\Users\andycunn\Documents\GitHub\SixDegrees\SixDegrees.Web\bundler\node_modules\handlebars\bin\handlebars template...

Options:
  -f, --output         Output File                                                           [string]
  -a, --amd            Exports amd style (require.js)                                        [boolean]
  -h, --handlebarPath  Path to handlebar.js (only valid for amd-style)                       [string]  [default: ""]
  -k, --known          Known helpers                                                         [string]
  -o, --knownOnly      Known helpers only                                                    [boolean]
  -m, --min            Minimize output                                                       [boolean]
  -s, --simple         Output template function only.                                        [boolean]
  -r, --root           Template root. Base value that will be stripped from template names.  [string]
        */

        private const string ArgumentsFormat = "-f {0} -a {1} -m {2} ";

        private ICollection<string> _knownHelpers = new List<string>();
        private string _nodeFileName;
        private string _outputFileName;
        private bool _shouldExportAMD;
        private bool _shouldMinify;
        private ICollection<string> _templateFileNames = new List<string>();
        private string _workingDirectory;

        public TemplateCompiler IncludeTemplateFiles(params string[] templateFileNames)
        {
            _templateFileNames = templateFileNames;
            return this;
        }

        public TemplateCompiler WithWorkingDirectory(string workingDirectory)
        {
            _workingDirectory = workingDirectory;
            return this;
        }

        public TemplateCompiler WithNodeJsExecutableFileName(string nodeFileName)
        {
            _nodeFileName = nodeFileName;
            return this;
        }

        public TemplateCompiler WithOutputFileName(string outputFileName)
        {
            _outputFileName = outputFileName;
            return this;
        }

        public TemplateCompiler WithKnownHelpers(params string[] helperNames)
        {
            _knownHelpers = helperNames;
            return this;
        }

        public TemplateCompiler WithMinification(bool shouldMinify = true)
        {
            _shouldMinify = shouldMinify;
            return this;
        }

        public TemplateCompiler WithAMDExport(bool shouldExportAMD = true)
        {
            _shouldExportAMD = shouldExportAMD;
            return this;
        }

        public string Compile()
        {
            ProcessStartInfo compileCommand = new ProcessStartInfo
            {
                FileName = _nodeFileName,
                Arguments = BuildArguments(),
                WorkingDirectory = _workingDirectory,
                CreateNoWindow = true
            };

            Process nodeProcess = new Process
            {
                StartInfo = compileCommand
            };

            this

            nodeProcess.Start();
            nodeProcess.WaitForExit();

            return File.ReadAllText(_outputFileName);
        }

        private string BuildArguments()
        {
            StringBuilder builder = new StringBuilder(
                string.Format(
                    ArgumentsFormat,
                    _outputFileName,
                    _shouldExportAMD,
                    _shouldMinify));

            foreach (string helper in _knownHelpers)
            {
                builder.AppendFormat("-k {0} ", helper);
            }

            builder.Append(string.Join(" ", _templateFileNames));

            return builder.ToString();
        }
    }
}