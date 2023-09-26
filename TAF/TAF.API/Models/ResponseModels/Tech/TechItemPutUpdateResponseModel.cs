using Newtonsoft.Json;
using TAF.API.Models.SharedModels.Tech;

namespace TAF.API.Models.ResponseModels.Tech
{
    public class TechItemPutUpdateResponseModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("data")]
        public TechData Data { get; set; }

        [JsonProperty("updatedAt")]
        public DateTime UpdatedAt { get; set; }
    }
}