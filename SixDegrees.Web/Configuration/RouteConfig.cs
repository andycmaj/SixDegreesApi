#region Imports

using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

#endregion

namespace SixDegrees.Web.Configuration
{
    public static class RouteConfig
    {
        private const string TypeConstraint = "movie|person";

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.IgnoreRoute("Views/Templates/{file}.html");

            /**
             * JSON API
             */

            routes.MapRoute("Search", "search/{type}/{query}",
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

            routes.MapRoute("Lookup", "{type}/{id}",
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

            routes.MapRoute("Home", "",
                new
                {
                    controller = "Home",
                    action = "Index"
                });
        }
    }
}