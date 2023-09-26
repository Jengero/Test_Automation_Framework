using TAF.Core.Utilities;
using TAF.Web.Pages;

namespace TAF.Tests.BDT.Steps
{
    [Binding]
    public class CookiesSteps
    {
        private MainPage _mainPage;

        [Given(@"I accept all cookies")]
        public void GivenIAcceptAllCookies()
        {
            _mainPage = new();
            Waiters.WaitForCondition(new Func<bool>(() => _mainPage.Cookies.Exists()));
            _mainPage.Cookies.AcceptAllCookiesButton.Click();
        }
    }
}