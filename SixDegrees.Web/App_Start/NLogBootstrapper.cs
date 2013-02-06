using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using LoggingExtensions.Logging;
using LoggingExtensions.NLog;

using NLog;
using NLog.Config;
using NLog.Layouts;
using NLog.Targets;

using SixDegrees.Web.App_Start;

[assembly: PreApplicationStartMethod(typeof(NLogBootstrapper), "Start")]

namespace SixDegrees.Web.App_Start
{
    public static class NLogBootstrapper
    {
        /// <summary>
        ///   Starts the application
        /// </summary>
        public static void Start()
        {
#if DEBUG
            // Setup the logging view for Sentinel - http://sentinel.codeplex.com
            var sentinalTarget = new NLogViewerTarget()
            {
                Name = "sentinal",
                Address = "udp://127.0.0.1:9999"
            };
            var sentinalRule = new LoggingRule("*", LogLevel.Trace, sentinalTarget);
            LogManager.Configuration.AddTarget("sentinal", sentinalTarget);
            LogManager.Configuration.LoggingRules.Add(sentinalRule);

            // Setup the logging view for Harvester - http://harvester.codeplex.com
            var harvesterTarget = new OutputDebugStringTarget()
            {
                Name = "harvester",
                Layout = new Log4JXmlEventLayout()
            };
            var harvesterRule = new LoggingRule("*", LogLevel.Trace, harvesterTarget);
            LogManager.Configuration.AddTarget("harvester", harvesterTarget);
            LogManager.Configuration.LoggingRules.Add(harvesterRule);

            LogManager.ReconfigExistingLoggers();
#endif

            // Configure this.Log() for NLog
            Log.InitializeWith<NLogLog>();
        }
    }
}