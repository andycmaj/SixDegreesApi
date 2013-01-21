using System.Collections.Generic;
using Newtonsoft.Json;

namespace SixDegrees.Data.Tmdb
{
    internal class Degree : IDegree
    {
        #region JSON Properties

        public string Name
        {
            get { return null; }
            set
            {
                Label = value;
                Type = DegreeType.Person;
            }
        }

        public string Title
        {
            get { return null; }
            set
            {
                Label = value;
                Type = DegreeType.Movie;
            }
        }

        [JsonProperty("profile_path")]
        public string ProfilePath
        {
            get { return null; }
            set
            {
                ThumbUrl = value;
                Type = DegreeType.Person;
            }
        }

        [JsonProperty("poster_path")]
        public string PosterPath
        {
            get { return null; }
            set
            {
                ThumbUrl = value;
                Type = DegreeType.Movie;
            }
        }

        public string Id { get; set; }
        public string Character { get; set; }

        #endregion JSON Properties

        public string Label { get; private set; }

        public string ThumbUrl { get; private set; }

        [JsonIgnore]
        public DegreeType Type { get; set; }

        public string InfoUrl { get; set; }

        public ICollection<IDegree> Children { get; private set; }

        public Degree()
        {
            Children = new List<IDegree>();
        }
    }
}