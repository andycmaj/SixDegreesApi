using System;
using System.Web;
using System.Web.Mvc;
using RestSharp.Serializers;

namespace SixDegrees.Web.Configuration.ActionResults
{
    public class ConfigurableJsonResult : JsonResult
    {
        private readonly ISerializer _serializer;

        public ConfigurableJsonResult(ISerializer serializer)
        {
            _serializer = serializer;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");
            if (JsonRequestBehavior == JsonRequestBehavior.DenyGet &&
                string.Equals(context.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException("JsonRequestBehavior set to DenyGet");
            HttpResponseBase response = context.HttpContext.Response;
            response.ContentType = string.IsNullOrEmpty(ContentType) ? "application/json" : ContentType;
            if (ContentEncoding != null)
                response.ContentEncoding = ContentEncoding;
            if (Data == null)
                return;

            response.Write(_serializer.Serialize(Data));
        }
    }
}