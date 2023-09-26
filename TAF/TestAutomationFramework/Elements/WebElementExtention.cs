using OpenQA.Selenium.Interactions;

namespace TAF.Core.Elements
{
    public static class WebElementExtention
    {
        public static void ClickUsingJS(this BaseElements element)
        {
            Browser.Browser.NewBrowser.ExecuteScript("arguments[0].click()", element.OriginalWebElement);
        }

        private static Actions CreateAction()
        {
            return Browser.Browser.NewBrowser.Action;
        }
    }
}