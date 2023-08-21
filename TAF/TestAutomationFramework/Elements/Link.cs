using OpenQA.Selenium;

namespace TAF.Core.Elements
{
    public class Link : BaseElements
    {
        public Link(By locator) : base(locator)
        {

        }

        public Link(IWebElement element) : base(element)
        {

        }
    }
}