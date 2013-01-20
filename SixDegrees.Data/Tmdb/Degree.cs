using System.Collections.Generic;
using Newtonsoft.Json;

namespace SixDegrees.Data.Tmdb
{
    internal class Degree : IDegree
    {
        #region JSON Properties

        public string Name { get; set; }
        public string Title { get; set; }

        [JsonProperty("profile_path")]
        public string ProfilePath { get; set; }

        [JsonProperty("poster_path")]
        public string PosterPath { get; set; }

        public string Id { get; set; }
        public string Character { get; set; }

        #endregion JSON Properties

        public string Label
        {
            get { return Name ?? Title; }
        }

        public string ThumbUrl
        {
            get { return ProfilePath ?? PosterPath; }
        }

        public DegreeType Type
        {
            get { return string.IsNullOrEmpty(Title) ? DegreeType.Person : DegreeType.Movie; }
        }

        public string InfoUrl { get; set; }

        public ICollection<IDegree> Children { get; private set; }

        public Degree()
        {
            Children = new List<IDegree>();
        }
    }
}