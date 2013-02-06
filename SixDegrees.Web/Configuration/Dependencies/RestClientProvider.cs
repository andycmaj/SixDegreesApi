using System.Net;
using Ninject.Activation;
using RestSharp;
using RestSharp.Deserializers;

namespace SixDegrees.Web.Configuration.Dependencies
{
    public class RestClientProvider : Provider<IRestClient>
    {
        private const string ApiUrl = "http://api.themoviedb.org/3";
        private const string ApiKey = "cd684dd007b56d859be21f1a4902b2b6";
        private const string JsonContentType = "application/json";

        private readonly IDeserializer _jsonDeserializer;
        private readonly bool _shouldUseFiddler;

        public RestClientProvider(IDeserializer jsonDeserializer, bool shouldUseFiddler)
        {
            _jsonDeserializer = jsonDeserializer;
            _shouldUseFiddler = shouldUseFiddler;
        }

        protected override IRestClient CreateInstance(IContext context)
        {
            var restClient = new RestClient(ApiUrl);
            restClient.AddDefaultParameter("api_key", ApiKey);

            restClient.AddDefaultHeader("Accept", JsonContentType);
            restClient.AddHandler(JsonContentType, _jsonDeserializer);

            if (_shouldUseFiddler)
            {
                restClient.Proxy = new WebProxy("localhost", 8888);
            }

            return restClient;
        }
    }
}