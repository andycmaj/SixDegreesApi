using System.Collections.Generic;
using System.Web.Mvc;
using SixDegrees.Data;

namespace SixDegrees.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDegreeRepository _degreeRepository;

        public HomeController(IDegreeRepository degreeRepository)
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
    }
}