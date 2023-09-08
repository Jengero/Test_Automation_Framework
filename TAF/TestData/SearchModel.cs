using System.Text.Json.Serialization;

namespace TestData
{
    public class SearchModel
    {
        [JsonPropertyName("KeywordOrJobId")]
        public string KeywordOrJobId;

        [JsonPropertyName("LocationCountry")]
        public string LocationCountry;

        [JsonPropertyName("LocationCity")]
        public string LocationCity;

        [JsonPropertyName("Skills")]
        public string Skills;

        [JsonPropertyName("SearchResult")]
        public string SearchResult;
    }
}