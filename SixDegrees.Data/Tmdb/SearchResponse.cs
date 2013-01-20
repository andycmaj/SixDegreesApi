using System.Collections.Generic;

namespace SixDegrees.Data.Tmdb
{
    internal class SearchResponse
    {
        public IEnumerable<Degree> Results { get; set; }
    }
}