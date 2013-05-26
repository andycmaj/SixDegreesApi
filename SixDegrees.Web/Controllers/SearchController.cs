using SixDegrees.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace SixDegrees.Web.Controllers
{
    public class SearchController : ApiController
    {
        private readonly IDegreeRepository _degreeRepository;

        public SearchController(IDegreeRepository degreeRepository)
        {
            _degreeRepository = degreeRepository;
        }

        // GET api/search?type={type}&query={query}s
        public IEnumerable<IDegree> Get(DegreeType type, string query)
        {
            IEnumerable<IDegree> degrees = _degreeRepository.FindDegrees(type, query);

            return degrees;
        }
    }
}
