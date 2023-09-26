using Newtonsoft.Json;

namespace TAF.API.Models.ResponseModels.Tech
{
    public class TechItemCreatedResponseModel : TechItemSingleResponseModel
    {
        [JsonProperty("createdAT")]
        public string CreatedAT { get; set; }
    }
}