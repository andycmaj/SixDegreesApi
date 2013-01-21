using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using RestSharp;
using SixDegrees.Data;
using SixDegrees.Data.Tmdb;

namespace SixDegrees.Tests
{
    [TestClass]
    public class TmdbSearchTests
    {
        private IDegreeRepository _searchService;

        [TestInitialize]
        public void TestInit()
        {
            var restClient = new RestClient("http://api.themoviedb.org/3");
            restClient.AddDefaultHeader("Accept", "application/json");
            restClient.AddDefaultParameter("api_key", "cd684dd007b56d859be21f1a4902b2b6");
            //restClient.Proxy = new WebProxy("localhost", 8888);

            var serializer = new JsonSerializer
                {
                    MissingMemberHandling = MissingMemberHandling.Ignore,
                    NullValueHandling = NullValueHandling.Ignore,
                    DefaultValueHandling = DefaultValueHandling.Include
                };
            restClient.AddHandler("application/json", new NewtonsoftJsonDeserializer(serializer));

            _searchService = new TmdbDegreeRepository(restClient);
        }

        [TestMethod]
        public void CanSearchForPerson()
        {
            IEnumerable<IDegree> degrees = _searchService.FindDegrees(DegreeType.Person, "Brad+Pitt");

            Assert.IsNotNull(degrees);
        }

        [TestMethod]
        public void CanSearchForMovie()
        {
            IEnumerable<IDegree> degrees = _searchService.FindDegrees(DegreeType.Movie, "Bad+Boys");

            Assert.IsNotNull(degrees);
        }

        [TestMethod]
        public void CanPopulateMovieCast()
        {
            var children = new List<IDegree>();
            var degree = new Mock<IDegree>();

            degree.Setup(mock => mock.Children).Returns(children);
            degree.Setup(mock => mock.Id).Returns("9737");
            degree.Setup(mock => mock.Type).Returns(DegreeType.Movie);

            _searchService.PopulateChildren(degree.Object);

            Assert.IsTrue(children.Any());
        }
    }
}