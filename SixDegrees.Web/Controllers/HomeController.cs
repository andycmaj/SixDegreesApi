using System.Web.Mvc;
using SixDegrees.Data;

namespace SixDegrees.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IDegreeRepository _degreeRepository;

        public HomeController(IDegreeRepository degreeRepository)
        {
            _degreeRepository = degreeRepository;
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}