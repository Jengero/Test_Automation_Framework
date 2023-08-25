using OpenQA.Selenium;
using TAF.Helper;

namespace TAF.Web.Pages
{
    public class MainPage : BasePage
    {
        public override string Url => TestSettings.ApplicationUrl;

        public HeaderPanel Header => new (By.XPath("//*[@class = 'header__content']"));
    }
}