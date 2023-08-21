using OpenQA.Selenium;

namespace TAF.Core.Browser
{
    public interface IDriverFactory
    {
        public IWebDriver GetDriver();
    }
}