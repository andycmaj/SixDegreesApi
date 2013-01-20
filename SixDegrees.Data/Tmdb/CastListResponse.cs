using System.Collections.Generic;
using Newtonsoft.Json;

namespace SixDegrees.Data.Tmdb
{
    internal class CastListResponse
    {
        [JsonProperty("cast")]
        public IEnumerable<Degree> Results { get; set; }
    }
}