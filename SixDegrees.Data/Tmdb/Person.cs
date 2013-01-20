using Newtonsoft.Json;

namespace SixDegrees.Data.Tmdb
{
    internal class Person : Degree
    {
        public override DegreeType Type
        {
            get { return DegreeType.Person; }
        }

        public override string ThumbUrl
        {
            get { return ProfilePath; }
        }

        public override string InfoUrl
        {
            get { return null; }
        }

        [JsonProperty("profile_path")]
        public string ProfilePath { get; set; }
    }
}