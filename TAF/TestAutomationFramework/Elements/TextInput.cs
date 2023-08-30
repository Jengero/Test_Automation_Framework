using OpenQA.Selenium;

namespace TAF.Core.Elements
{
    public class TextInput : BaseElements
    {
        public TextInput(By element) : base(element)
        {
        }

        public TextInput(IWebElement element) : base(element)
        {

        }

    }
}