using RestSharp;

namespace TAF.API.Controllers
{
    public class TechController : BaseController
    {
            public TechController(CustomRestClient client) : base(client, Service.Tech)
            {

            }

            private const string AllTechResource = "/objects";

            /// <summary>
            /// Gets list of Bibles from API
            /// </summary>
            /// <typeparam name="T"><see cref="AllObjectsModel"> </typeparam>
            /// <returns>response info <see cref="RestResponse"> and <see cref="AllObjectsModel"></returns>
            public (RestResponse response, T? Tech) GetTech<T>()
            {
                return Get<T>(AllTechResource);
            }
    }
}