using Newtonsoft.Json;
using TAF.API.Models.SharedModels.Tech;

namespace TAF.API.Models.ResponseModels.Tech
{
    public class TechItemSingleResponseModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("data")]
        public TechData Data { get; set; }
    }
}