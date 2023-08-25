namespace TAF.Core.Elements
{
    public static class WebElementExtension
    {
        public static void ClickUsingJS(this BaseElements element)
        {
            Browser.Browser.NewBrowser.ExecuteScript("arguments[0].click()", element.OriginalWebElement);
        }
    }
}