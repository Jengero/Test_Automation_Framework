using OpenQA.Selenium;

namespace TAF.Core.Elements
{
    public class BasePanel : BaseElements
    {
        public BasePanel(By locator) : base(locator)
        {

        }

        public BasePanel(IWebElement element) : base(element)
        {

        }
    }
}