using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace TAF.Core.Helpers
{
    public static class ActionsHelper
    {
        public static Actions ScrollToElement(IWebElement element) => Browser.Browser.NewBrowser.Action.ScrollToElement(element);

        public static Actions MoveToElement(IWebElement element) => Browser.Browser.NewBrowser.Action.MoveToElement(element);

        public static void PerformAction(this Actions actions) 
        {
         actions.Build().Perform();
        }
    }
}