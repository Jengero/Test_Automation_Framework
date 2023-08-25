using OpenQA.Selenium;
using System.Collections.ObjectModel;
using TAF.Core.Browser;
using TAF.Core.Elements;
using TAF.Core.Utilities;

namespace TAF.Web.Pages
{
    public abstract class BasePage
    {
        public abstract string Url { get; }

        public Button AcceptAllCookiesButton => new Button(By.XPath("//*[@id = 'onetrust-accept-btn-handler']"));
        public HtmlElement CookiesFrame => new(By.Id("onetrust-banner-sdk"));

        public bool IsOpened()
        {
            return Browser.NewBrowser.GetUrl().Equals(Url);
        }

        public void AcceptAllCookies()
        {
            Waiters.WaitForCondition(new Func<bool>(() => CookiesFrame.Exists()));
            AcceptAllCookiesButton.Click();
        }

        public IWebElement FindElement(By by)
        {
            return Browser.NewBrowser.FindElement(by);
        }

        public ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            return Browser.NewBrowser.FindElements(by);
        }
    }
}