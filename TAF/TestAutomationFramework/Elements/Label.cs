using OpenQA.Selenium;

namespace TAF.Core.Elements
{
    public class Label : BaseElements
    {
        public Label(By locator) : base(locator)
        {
        }

        public Label(IWebElement element) : base(element)
        {

        }
    }
}