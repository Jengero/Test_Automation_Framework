using OpenQA.Selenium;

namespace TAF.Core.Elements
{
    public class Button : BaseElements
    {
        public Button(By locator) : base(locator)
        {

        }

        public Button(IWebElement element) : base(element)
        {

        }
    }
}