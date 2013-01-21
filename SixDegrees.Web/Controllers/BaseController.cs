using System.Text;
using System.Web.Mvc;
using Ninject;
using SixDegrees.Web.Configuration.ActionResults;

namespace SixDegrees.Web.Controllers
{
    public class BaseController : Controller
    {
        [Inject]
        public ConfigurableJsonResult JsonResult { private get; set; }

        protected override JsonResult Json(object data, string contentType, Encoding contentEncoding,
                                           JsonRequestBehavior behavior)
        {
            JsonResult.Data = data;
            JsonResult.ContentEncoding = contentEncoding;
            JsonResult.ContentType = contentType;
            JsonResult.JsonRequestBehavior = behavior;

            return JsonResult;
        }
    }
}