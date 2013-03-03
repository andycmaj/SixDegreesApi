using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

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

        /// <summary>
        /// Get the underlying JsonSerializer instance used by the provided serializer instances.
        /// </summary>
        /// <returns></returns>
        public JsonSerializer GetSerializer()
        {
            if (_serializer == null)
            {
                _serializer = new JsonSerializer
                {
                    MissingMemberHandling = MissingMemberHandling.Ignore,
                    NullValueHandling = NullValueHandling.Ignore,
                    DefaultValueHandling = DefaultValueHandling.Include,
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };

                _serializer.Converters.Add(new IsoDateTimeConverter());
            }

            return _serializer;
        }

        public object Create(IContext context)
        {
            return new NewtonsoftJsonDeserializer(GetSerializer());
        }

        public Type Type { get { return typeof (NewtonsoftJsonDeserializer); }}
    }
}