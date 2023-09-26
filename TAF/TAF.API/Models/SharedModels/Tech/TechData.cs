using Newtonsoft.Json;

namespace TAF.API.Models.SharedModels.Tech
{
    public class TechData
    {
        [JsonProperty("color")]
        public string Color { get; set; }

        [JsonProperty("capacity")]
        public string Capacity { get; set; }

        [JsonProperty("capacityGB")]
        public int CapacityGB { get; set; }

        [JsonProperty("price")]
        public float Price { get; set; }

        [JsonProperty("generation")]
        public string Generation { get; set; }

        [JsonProperty("year")]
        public int Year { get; set; }

        [JsonProperty("CPUmodel")]
        public string CPUmodel { get; set; }

        [JsonProperty("hardDiskSize")]
        public string Harddisksize { get; set; }

        [JsonProperty("StrapColour")]
        public string StrapColour { get; set; }

        [JsonProperty("CaseSize")]
        public string CaseSize { get; set; }

        [JsonProperty("Color")]
        public string ColorToUpper { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonProperty("Capacity")]
        public string CapacityToUpper { get; set; }

        [JsonProperty("ScreenSize")]
        public float Screensize { get; set; }

        [JsonProperty("Generation")]
        public string GenerationToUpper { get; set; }

        [JsonProperty("Price")]
        public string PriceToUpper { get; set; }
    }
}