using Newtonsoft.Json;

namespace TAF.API.Models.ResponseModels.Tech
{
    public class TechItemDeleteResponseModel
    {
        [JsonProperty("message")]
        public string DeletionMessage { get; set; }
    }
}