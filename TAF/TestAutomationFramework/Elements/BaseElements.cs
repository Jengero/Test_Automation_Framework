using OpenQA.Selenium;
using System.Collections.ObjectModel;
using TAF.Utilities.Logger;

namespace TAF.Core.Elements
{
    public class BaseElements : IBaseElements
    {
        private readonly IWebElement _element;

        public BaseElements()
        {
            
        }

        public BaseElements(By locator)
        {
            _element = Browser.Browser.NewBrowser.FindElement(locator);
        }


        public BaseElements(IWebElement element)
        {
            _element = element;
        }

        public IWebElement OriginalWebElement => _element;

        public string GetText() => OriginalWebElement.Text.Trim();

        public string GetAttribute(string attributeName) => OriginalWebElement.GetAttribute(attributeName);

        public string GetProperty(string propertyName) => OriginalWebElement.GetDomProperty(propertyName);

        public virtual void Click()
        {
            Logger.Info("Click to element"); 
            OriginalWebElement.Click();
        }

        public void SendKeys(string text)
        {
            Logger.Info("Send keys to field");
            OriginalWebElement.SendKeys(text);
        }

        public void Clear()
        {
            Logger.Info("Clear the text");
            OriginalWebElement.Clear();
        }

        public bool Exists()
        {
            Logger.Info("Check, if element exist");
            return OriginalWebElement != null;
        }

        public bool IsDisplayed() => OriginalWebElement.Displayed;

        public bool IsEnabled() => OriginalWebElement.Enabled;

        public IWebElement FindElement(By by) => OriginalWebElement.FindElement(by);

        public ReadOnlyCollection<IWebElement> FindElements(By by) => OriginalWebElement.FindElements(by);
    }
}