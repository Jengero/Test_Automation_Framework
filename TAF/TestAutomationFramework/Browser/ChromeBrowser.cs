using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace TAF.Core.Browser
{
    public class ChromeBrowser : IDriverFactory
    {
        public IWebDriver GetDriver()
        {
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArgument("--start-maximized");
            //chromeOptions.AddArgument("Headless");
            chromeOptions.AddArgument("window-size=1920,1080");
            var service = ChromeDriverService.CreateDefaultService();
            var chromeDriver = new ChromeDriver(service, chromeOptions, TimeSpan.FromMinutes(3));
            return chromeDriver;
        }
    }
}