using System.Collections.Generic;

namespace SixDegrees.Data
{
    public interface IDegree
    {
        DegreeType Type { get; }
        string Label { get; }
        string Character { get; }
        string Id { get; }
        string ThumbUrl { get; }
        string InfoUrl { get; }
        ICollection<IDegree> Children { get; }
    }
}