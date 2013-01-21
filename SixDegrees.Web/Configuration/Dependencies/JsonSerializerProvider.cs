using System;
using Newtonsoft.Json;
using Ninject.Activation;
using RestSharp.Deserializers;
using RestSharp.Serializers;
using SixDegrees.Data;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace SixDegrees.Web.Configuration.Dependencies
{
    public class JsonSerializerProvider : IProvider<IDeserializer>, IProvider<ISerializer>, IProvider
    {
        private JsonSerializer _serializer;

        public JsonSerializer GetSerializer()
        {
            return _serializer;
        }

        public object Create(IContext context)
        {
            _serializer = new JsonSerializer
            {
                MissingMemberHandling = MissingMemberHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Include
            };

            return new NewtonsoftJsonDeserializer(_serializer);
        }

        public Type Type { get { return typeof (NewtonsoftJsonDeserializer); }}
    }
}