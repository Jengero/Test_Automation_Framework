namespace TAF.Core.Elements
{
    public interface IBaseElements
    {
        string GetText();
        string GetAttribute(string attributeName);

        void Click();
        void SendKeys(string text);
        void Clear();
    }
}