using RestSharp;
using System.Net;
using TAF.API.Controllers;
using TAF.API.Models.ResponseModels.Bibles;

namespace TAF.API.Tests
{
    [TestFixture]
    public class BiblesTest
    {

        [Test]
        public void CheckAllBiblesResponseWithValidParams() 
        {
            var response = new BiblesController(new CustomRestClient()).GetBibles<RestResponse>();
            
            Assert.That(response.response.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Invalid response status code from /v1/bibles");
        }

        [Test]
        public void CheckAllBiblesResponseWithoutAuthorization()
        {
            var response = new BiblesController(new CustomRestClient(), string.Empty).GetBibles<RestResponse>();
            
            Assert.That(response.response.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized), "Invalid response status without authorization from /v1/bibles");
        }

        [Test]
        public void CheckAllBiblesResponse()
        {
            var response = new BiblesController(new CustomRestClient()).GetBibles<AllBiblesModel>();

            CollectionAssert.IsNotEmpty(response.Bibles.data, "No objects from /v1/bibles were returned");
        }

        [Test]
        public void CheckAudioBiblesResponseWithValidParams()
        {
            var response = new BiblesController(new CustomRestClient()).GetAudioBibles<RestResponse>();

            Assert.That(response.response.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Invalid response status code from /v1/audio-bibles");
        }

        [Test]
        public void CheckResponseAllBooksFromAudioBibleWithValidParams()
        {
            var audioBibleId = new BiblesController(new CustomRestClient()).GetAudioBibles<AllBiblesModel>().Bibles.data.Select(p => p.id).FirstOrDefault();
            var response = new BiblesController(new CustomRestClient()).GetBookFromAudioBible<AllBooksFromAudioBibleModel>(audioBibleId);
            
            Assert.That(response.response.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Invalid response status code from /v1/audio-bibles/{audioBibleId}/books");
        }
    }
}