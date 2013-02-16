#region Imports

using System;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

using LoggingExtensions.Logging;
using LoggingExtensions.NLog;

using Microsoft.Web.Infrastructure.DynamicModuleHelper;

using Ninject;
using Ninject.Web.Common;

using SixDegrees.Web.App_Start;
using SixDegrees.Web.Configuration;
using SixDegrees.Web.Configuration.Bundles;
using SixDegrees.Web.Configuration.Dependencies;

using WebActivator;

using NinjectBootstrapper = Ninject.Web.Common.Bootstrapper;

#endregion

[assembly: PreApplicationStartMethod(typeof (AppBootstrapper), "Start")]
[assembly: ApplicationShutdownMethod(typeof (AppBootstrapper), "Stop")]
[assembly: PostApplicationStartMethod(typeof (AppBootstrapper), "AfterStart")]

namespace SixDegrees.Web.App_Start
{
    public static class AppBootstrapper
    {
        private static readonly NinjectBootstrapper KernelBootstrapper = new NinjectBootstrapper();

        /// <summary>
        ///   Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof (OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof (NinjectHttpModule));
            KernelBootstrapper.Initialize(CreateKernel);
        }

        public static void AfterStart()
        {
            IKernel kernel = KernelBootstrapper.Kernel;

            foreach (var configModule in kernel.GetAll<IWebConfigurationModule>())
            {
                configModule.Configure();
            }
        }

        /// <summary>
        ///   Stops the application.
        /// </summary>
        public static void Stop()
        {
            KernelBootstrapper.ShutDown();
        }

        /// <summary>
        ///   Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            StandardKernel kernel = new StandardKernel(new DependencyConfig(), new ConfigurationModules());
            kernel.Bind<Func<IKernel>>()
                  .ToMethod(ctx => () => kernel);

            return kernel;
        }
    }
}