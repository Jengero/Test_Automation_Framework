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

        public bool IsOpened()
        {
            return Browser.NewBrowser.GetUrl().Equals(Url);
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