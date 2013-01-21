using System.Collections.Generic;

namespace SixDegrees.Data
{
    public interface IDegreeRepository
    {
        /// <summary>
        ///     Find Degrees matching a specified keyWord.
        /// </summary>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        IEnumerable<IDegree> FindDegrees(DegreeType type, string keyWord);

        /// <summary>
        ///     Populat child degrees for a specified degree.
        /// </summary>
        /// <param name="degree"></param>
        /// <param name="depth"></param>
        void PopulateChildren(IDegree degree, int depth = 1);

        IDegree GetDegree(DegreeType type, string id);
    }
}