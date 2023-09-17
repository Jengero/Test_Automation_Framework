using RestSharp;
using System.Globalization;
using TAF.API.Models.ResponseModels.Bibles;

namespace TAF.API.Controllers
{
    public class BiblesController : BaseController
    {
        private const string AllBiblesResource = "/v1/bibles";
        private const string AllAudioBiblesResource = "/v1/audio-bibles";

        public BiblesController(CustomRestClient client, string apiKey) : base(client, Service.Bibles, apiKey)
        {

        }

        public BiblesController(CustomRestClient client) : base(client, Service.Bibles, client.AppConfiguration.ApiKey) 
        {

        }

        /// <summary>
        /// Gets list of Bibles from API
        /// </summary>
        /// <typeparam name="T"><see cref="AllBiblesModel"> </typeparam>
        /// <returns>response info <see cref="RestResponse"> and <see cref="AllBiblesModel"></returns>
        public (RestResponse response, T? Bibles) GetBibles<T>() 
        {
            return Get<T>(AllBiblesResource);
        }

        /// <summary>
        /// Gets list of Audio-bibles from API
        /// </summary>
        /// <typeparam name="T"><see cref="AllBiblesModel"></typeparam>
        /// <returns>response info <see cref="RestResponse"> and <see cref="AllBiblesModel"</returns>
        public (RestResponse response, T? Bibles) GetAudioBibles<T>()
        {
            return Get<T>(AllAudioBiblesResource);
        }

        /// <summary>
        /// Gets list of Books of current Audio-bible from API
        /// </summary>
        /// <typeparam name="T"><see cref="AllBooksFromAudioBibleModel"></typeparam>
        /// <param name="audioBibleId"></param>
        /// <returns></returns>
        public (RestResponse response, T? Bibles) GetBookFromAudioBible<T>(string audioBibleId)
        {
            return Get<T>($"/v1/audio-bibles/{audioBibleId}/books");
        }
    }
}