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

        // GET api/degrees
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/degrees/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/degrees
        public void Post([FromBody]string value)
        {
        }

        // PUT api/degrees/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/degrees/5
        public void Delete(int id)
        {
        }
    }
}
