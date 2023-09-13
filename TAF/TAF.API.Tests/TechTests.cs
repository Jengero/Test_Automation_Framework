using RestSharp;
using System.Net;
using TAF.API.Controllers;
using TAF.API.Models.ResponseModels;

namespace TAF.API.Tests
{
    [TestFixture]
    public class TechTests
    {
        [Test]
        public void AllTechResponseWithValidParamsTest()
        {
            var response = new TechController(new CustomRestClient()).GetTech<RestResponse>();

            Assert.That(response.response.StatusCode, Is.EqualTo(HttpStatusCode.OK), $"Invalid status code ({response.response.StatusCode})");
        }

        [Test]
        public void AllTechResponseTest()
        {
            var response = new TechController(new CustomRestClient()).GetTech<List<AllObjectsModel>>();

            CollectionAssert.IsNotEmpty(response.Tech, "Any object should be returned");
        }

        [Test]
        public void QuantityOfObjectsAllTechResponseTest()
        {
            var currentIdNumber = 13;
            var response = new TechController(new CustomRestClient()).GetTech<List<AllObjectsModel>>().Tech.Select(f => f.id);

            Assert.That(response.Count, Is.EqualTo(currentIdNumber), $"The number of objects is not equal");
        }
    }
}