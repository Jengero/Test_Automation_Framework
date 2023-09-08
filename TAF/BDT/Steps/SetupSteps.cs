using NUnit.Framework;
using TAF.Core.Browser;
using TAF.Core.Utilities;
using TAF.Helper;
using TAF.Utilities.Logger;
using TAF.Web.Pages;

namespace TAF.Tests.BDT.Steps
{
    [Binding]
    public class SetupSteps
    {
        private MainPage _mainPage;

        [BeforeScenarioBlock]
        public void LoggerSetup() => Logger.NewLogger(TestContext.CurrentContext.Test.Name, TestContext.CurrentContext.TestDirectory);

        [Given(@"navigation to epam\.com page")]
        public void GivenNavigationToEpam_ComPage()
        {
            _mainPage = new();
            Logger.Info("Start new test");
            Browser.NewBrowser.GoToUrl(TestSettings.ApplicationUrl);
            Waiters.WaitForPageLoad();
        }

        [Given(@"accept all cookies")]
        public void GivenAcceptAllCookies()
        {
            Waiters.WaitForCondition(new Func<bool>(() => _mainPage.Cookies.Exists()));
            _mainPage.Cookies.AcceptAllCookiesButton.Click();
        }

        [AfterScenario]
        public void QuitBrowser()
        {
            if (TestContext.CurrentContext.Result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Failed)
            {
                Logger.Info("Test is failed");
            }
            Logger.Info("Test finish");
            Browser.CloseBrowser();
        }
    }
}