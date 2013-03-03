using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using SixDegrees.Web.Configuration.Dependencies;

namespace SixDegrees.Web.Configuration
{
    public class JsonNetFormatter : MediaTypeFormatter
    {
        private readonly JsonSerializer _serializer;

        public JsonNetFormatter(JsonSerializerProvider serializerProvider)
        {
            _serializer = serializerProvider.GetSerializer();
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/json"));
        }

        public override bool CanWriteType(Type type)
        {
            // don't serialize JsonValue structure use default for that
            if (type == typeof (JValue) || type == typeof (JObject) || type == typeof (JArray))
            {
                return false;
            }

            return true;
        }

        public override bool CanReadType(Type type)
        {
            return true;
        }

        public override Task<object> ReadFromStreamAsync(Type type,
                                                         Stream stream,
                                                         HttpContent content,
                                                         IFormatterLogger formatterLogger)
        {
            return Task<object>.Factory.StartNew(() =>
            {
                using (var sr = new StreamReader(stream))
                {
                    using (var jreader = new JsonTextReader(sr))
                    {
                        return _serializer.Deserialize(jreader, type);
                    }
                }
            });
        }

        public override Task WriteToStreamAsync(Type type,
                                                object value,
                                                Stream stream,
                                                HttpContent content,
                                                TransportContext transportContext)
        {
            return Task.Factory.StartNew(() =>
            {
                using (var sw = new StreamWriter(stream))
                {
                    using (var jw = new JsonTextWriter(sw))
                    {
                        _serializer.Serialize(jw, value);
                    }
                }
            });
        }
    }
}