using Newtonsoft.Json;
using RestSharp;

namespace TAF.API
{
    public class BaseController
    {
        private readonly RestClient _restClient;

        public BaseController(CustomRestClient client, Service service, string apiKey = "")
        {
            _restClient = client.CreateRestClient(service);

            if (service == Service.Bibles)
                _restClient.AddDefaultHeader("api-key", apiKey);
        }

        public (RestResponse Response, T ResponseModel) Get<T>(string resource)
        {
            var request = new RestRequest(resource, Method.Get);
            var response = _restClient.ExecuteGet(request);

            return typeof(T) == typeof(RestResponse)
                ? (response, default)
                : (response, GetDeserializedView<T>(response));
        }

        private T GetDeserializedView<T>(RestResponse response)
        {
            return JsonConvert.DeserializeObject<T>(response.Content);
        }
    }
}