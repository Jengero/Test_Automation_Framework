using Newtonsoft.Json;
using TAF.API.Models.SharedModels.Tech;

namespace TAF.API.Models.RequestModels
{
    public class TechItemRequestModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("data")]
        public TechData Data { get; set; }
    }
}