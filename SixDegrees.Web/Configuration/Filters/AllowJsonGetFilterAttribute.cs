using System.Web.Mvc;

namespace SixDegrees.Web.Configuration.Filters
{
    public class AllowJsonGetFilterAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            var result = filterContext.Result as JsonResult;

            if (result != null)
            {
                result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            }
        }
    }
}