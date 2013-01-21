using System.Web.Mvc;
using SixDegrees.Web.Configuration.Filters;

namespace SixDegrees.Web
{
    public static class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new AllowJsonGetFilterAttribute());
            filters.Add(new HandleErrorAttribute());
        }
    }
}