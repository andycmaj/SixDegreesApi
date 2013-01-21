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