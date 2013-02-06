#region Imports

using System.Linq;

using SixDegrees.Web.App_Start;

using Spark;
using Spark.Web.Mvc;

using WebActivator;

#endregion

[assembly: PreApplicationStartMethod(typeof (SparkBootstrapper), "Start")]

namespace SixDegrees.Web.App_Start
{
    public static class SparkBootstrapper
    {
        public static void Start()
        {
            SparkSettings settings = new SparkSettings()
                .SetAutomaticEncoding(true);

            // Instead of _global.spark, we configure namespaces and assemblies here.
            ConfigureViewImports(settings);

            SparkEngineStarter.RegisterViewEngine(settings);
        }

        private static void ConfigureViewImports(SparkSettings settings)
        {
            settings.AddAssembly("Spark.Web.Mvc")
                    .AddAssembly("System.Web.Mvc, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35")
                    .AddAssembly("System.Web.Routing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35")
                    .AddAssembly("System.Web.Optimization, Version=1.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35");

            settings.AddNamespace("System")
                    .AddNamespace("System.Collections.Generic")
                    .AddNamespace("System.Linq")
                    .AddNamespace("System.Web.Mvc")
                    .AddNamespace("System.Web.Mvc.Html")
                    .AddNamespace("System.Web.Optimization");
        }
    }
}