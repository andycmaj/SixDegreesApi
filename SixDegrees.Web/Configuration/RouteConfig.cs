using System.Web.Mvc;
using System.Web.Routing;

namespace SixDegrees.Web
{
    public static class RouteConfig
    {
        private const string TypeConstraint = "movie|person";

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("Search", "search/{type}/{query}",
                            new
                                {
                                    controller = "Home",
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
        }
    }
}