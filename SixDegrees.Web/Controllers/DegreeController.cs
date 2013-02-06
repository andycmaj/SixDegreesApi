using System.Collections.Generic;
using System.Web.Mvc;
using SixDegrees.Data;

namespace SixDegrees.Web.Controllers
{
    public class DegreeController : BaseController
    {
        private readonly IDegreeRepository _degreeRepository;

        public DegreeController(IDegreeRepository degreeRepository)
        {
            _degreeRepository = degreeRepository;
        }

        public ActionResult Search(DegreeType type, string query)
        {
            IEnumerable<IDegree> degrees = _degreeRepository.FindDegrees(type, query);

            return Json(new
                {
                    results = degrees
                });
        }

        public ActionResult Lookup(DegreeType type, string id)
        {
            IDegree degree = _degreeRepository.GetDegree(type, id);
            _degreeRepository.PopulateChildren(degree);

            return Json(new
                {
                    results = degree.Children
                });
        }
    }
}