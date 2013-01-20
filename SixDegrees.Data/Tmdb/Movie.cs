using Newtonsoft.Json;

namespace SixDegrees.Data.Tmdb
{
    internal class Movie : Degree
    {
        public override DegreeType Type
        {
            get { return DegreeType.Movie; }
        }

        public override string ThumbUrl
        {
            get { return ProfilePath; }
        }

        public override string InfoUrl
        {
            get { return null; }
        }

        [JsonProperty("title")]
        public override string Name { get; set; }

        [JsonProperty("poster_path")]
        public string ProfilePath { get; set; }
    }
}