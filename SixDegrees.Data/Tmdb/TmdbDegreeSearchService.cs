using System.Collections.Generic;
using RestSharp;

namespace SixDegrees.Data.Tmdb
{
    public class TmdbDegreeSearchService : IDegreeSearchService
    {
        private const string SearchResourceTemplate = "search/{ResourceType}";

        private const string LookupResourceTemplate = "{ResourceType}/{Id}/{PropertyType}";

        private readonly IRestClient _restClient;

        public TmdbDegreeSearchService(IRestClient restClient)
        {
            _restClient = restClient;
        }

        public IEnumerable<IDegree> FindDegrees(DegreeType type, string keyWord)
        {
            IRestRequest request = new RestRequest(SearchResourceTemplate);
            request.AddUrlSegment("ResourceType", type.ResourceName);
            request.AddParameter("query", keyWord);

            IRestResponse<SearchResponse> response = _restClient.Execute<SearchResponse>(request);

            return response.Data.Results;
        }

        public void PopulateChildren(IDegree degree, int depth = 1)
        {
            IRestRequest request = new RestRequest(LookupResourceTemplate);
            request.AddUrlSegment("ResourceType", degree.Type.ResourceName);
            request.AddUrlSegment("Id", degree.Id);
            request.AddUrlSegment("PropertyType", degree.Type.ChildDegreePropertyName);

            IRestResponse<CastListResponse> response = _restClient.Execute<CastListResponse>(request);
            foreach (Degree movie in response.Data.Results)
            {
                degree.Children.Add(movie);
            }
        }
    }
}