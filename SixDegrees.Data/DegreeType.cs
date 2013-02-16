using System;
using System.ComponentModel;

namespace SixDegrees.Data
{
    [TypeConverter(typeof(DegreeTypeTypeConverter))]
    public class DegreeType
    {
        public static DegreeType Person = new DegreeType("person", "credits");
        public static DegreeType Movie = new DegreeType("movie", "casts");

        public string ResourceName { get; private set; }
        public string ChildDegreePropertyName { get; private set; }

        private DegreeType(string resourceName, string childDegreePropertyName)
        {
            ResourceName = resourceName;
            ChildDegreePropertyName = childDegreePropertyName;
        }

        public static DegreeType Parse(string name)
        {
            switch (name.ToLower())
            {
                case "person":
                    return Person;
                case "movie":
                    return Movie;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}