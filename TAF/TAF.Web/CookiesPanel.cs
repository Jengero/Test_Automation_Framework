using OpenQA.Selenium;
using TAF.Core.Elements;

namespace TAF.Web
{
    public class CookiesPanel : BasePanel
    {
        public Button AcceptAllCookiesButton => new(By.XPath("//*[@id = 'onetrust-accept-btn-handler']"));
        public CookiesPanel CookiesFrame => new(By.Id("onetrust-banner-sdk"));

        public CookiesPanel(By locator) : base(locator) { }
    }
}