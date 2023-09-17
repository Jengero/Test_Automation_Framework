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

        protected (RestResponse Response, T ResponseModel) Get<T>(string resource)
        {
            var request = new RestRequest(resource, Method.Get);
            var response = _restClient.ExecuteGet(request);

            return typeof(T) == typeof(RestResponse)
                ? (response, default)
                : (response, GetDeserializedView<T>(response));
        }

        protected (RestResponse Response, T ResponseModel) Post<T, TPayload>(string resource, TPayload payload) where TPayload : class
        {
            var request = new RestRequest(resource, Method.Post);
            request.AddJsonBody(payload);

            var response = _restClient.ExecutePost(request);

            return typeof(T) == typeof(RestResponse)
                ? (response, default)
                : (response, GetDeserializedView<T>(response));
        }

        protected (RestResponse Response, T ResponseModel) Delete<T>(string resource)
        {
            var request = new RestRequest(resource, Method.Delete);
            var response = _restClient.Delete(request);

            return typeof(T) == typeof(RestResponse)
                ? (response, default)
                : (response, GetDeserializedView<T>(response));
        }

        protected (RestResponse Response, T ResponseModel) Put<T, TPayload>(string resource, TPayload payload) where TPayload : class
        {
            var request = new RestRequest(resource, Method.Put);
            request.AddJsonBody(payload);

            var response = _restClient.Put(request);

            return typeof(T) == typeof(RestResponse)
                ? (response, default)
                : (response, GetDeserializedView<T>(response));
        }

        protected (RestResponse Response, T?) Patch<T, TPayload>(string resource, Object payload) where TPayload : class
        {
            var request = new RestRequest(resource, Method.Patch);
            request.AddJsonBody(payload);

            var response = _restClient.Patch(request);

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