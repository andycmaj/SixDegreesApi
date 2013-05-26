using SixDegrees.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SixDegrees.Web.Controllers
{
    public class DegreesController : ApiController
    {
        private readonly IDegreeRepository _degreeRepository;

        public DegreesController(IDegreeRepository degreeRepository)
        {
            _degreeRepository = degreeRepository;
        }

        // GET api/degrees/{type}/{id}
        public IEnumerable<IDegree> Get(DegreeType type, string id)
        {
            IDegree degree = _degreeRepository.GetDegree(type, id);
            _degreeRepository.PopulateChildren(degree);

            return degree.Children;
        }
    }
}
