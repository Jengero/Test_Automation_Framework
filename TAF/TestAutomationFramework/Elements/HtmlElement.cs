using OpenQA.Selenium;

namespace TAF.Core.Elements
{
    public class HtmlElement : BaseElements
    {
        public HtmlElement(By locator) : base(locator)
        {

        }

        public HtmlElement(IWebElement element) : base(element)
        {

        }
    }
}