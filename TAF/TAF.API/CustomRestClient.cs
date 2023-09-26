using Microsoft.Extensions.Configuration;
using RestSharp;

namespace TAF.API
{
    public class CustomRestClient
    {
        public readonly AppConfiguration AppConfiguration = new();

        public CustomRestClient() 
        {
        var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            config.Bind(AppConfiguration);
        }

        public RestClient CreateRestClient(Service service) 
        {
            var baseUrl = service switch
            {
                Service.Bibles => AppConfiguration.BiblesBaseUrl,
                Service.Tech => AppConfiguration.TechBaseUrsl,
                _ => throw new ArgumentException("Wrong service option provided")
            };

            var options = new RestClientOptions(baseUrl);

            return new RestClient(options);
        }
    }
}