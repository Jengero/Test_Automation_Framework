using RestSharp;
using System.Net;
using TAF.API.Controllers;
using TAF.API.Models.ResponceModels;
using TAF.API.Models.ResponseModels;

namespace TAF.API.Tests
{
    [TestFixture]
    public class BiblesTest
    {

        [Test]
        public void CheckAllBiblesResponseWithValidParams() 
        {
            var response = new BiblesController(new CustomRestClient()).GetBibles<RestResponse>();
            
            Assert.That(response.response.StatusCode, Is.EqualTo(HttpStatusCode.OK), $"Invalid status code ({response.response.StatusCode})!");
        }

        [Test]
        public void CheckAllBiblesResponseWithoutAuthorization()
        {
            var response = new BiblesController(new CustomRestClient(), string.Empty).GetBibles<RestResponse>();
            
            Assert.That(response.response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized), $"Invalid status code ({response.response.StatusCode})!");
        }

        [Test]
        public void CheckAllBiblesResponse()
        {
            var response = new BiblesController(new CustomRestClient()).GetBibles<AllBiblesModel>();

            CollectionAssert.IsNotEmpty(response.Bibles.data, "Any bible should be returned!");
        }

        [Test]
        public void CheckAudioBiblesResponseWithValidParams()
        {
            var response = new BiblesController(new CustomRestClient()).GetAudioBibles<RestResponse>();

            Assert.That(response.response.StatusCode, Is.EqualTo(HttpStatusCode.OK), $"Invalid status code ({response.response.StatusCode})!");
        }

        [Test]
        public void CheckResponseAllBooksFromAudioBibleWithValidParams()
        {
            var audioBibleId = new BiblesController(new CustomRestClient()).GetAudioBibles<AllBiblesModel>().Bibles.data.Select(p => p.id).FirstOrDefault();
            var response = new BiblesController(new CustomRestClient()).GetBookFromAudioBible<AllBooksFromAudioBibleModel>(audioBibleId);
            
            Assert.That(response.response.StatusCode, Is.EqualTo(HttpStatusCode.OK), $"Invalid status code ({response.response.StatusCode})!");
        }
    }
}