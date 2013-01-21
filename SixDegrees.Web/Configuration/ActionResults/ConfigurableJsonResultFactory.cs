using Ninject.Activation;
using RestSharp.Serializers;

namespace SixDegrees.Web.Configuration.ActionResults
{
    public class ConfigurableJsonResultFactory : Provider<ConfigurableJsonResult>
    {
        private readonly ISerializer _serializer;

        public ConfigurableJsonResultFactory(ISerializer serializer)
        {
            _serializer = serializer;
        }

        protected override ConfigurableJsonResult CreateInstance(IContext context)
        {
            return new ConfigurableJsonResult(_serializer);
        }
    }
}