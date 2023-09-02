using TAF.Core.Browser;
using TAF.Core.Utilities;
using TAF.Helper;
using TAF.Utilities.Logger;
using TAF.Web.Pages;

namespace TAF.Tests
{
    public abstract class BaseTest
    {
        private MainPage _mainPage;
        public TestContext TestContext { get; set; }

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Logger.NewLogger(TestContext.CurrentContext.Test.Name, TestContext.CurrentContext.TestDirectory);
        }

        [SetUp]
        public virtual void BeforeTest()
        {
            _mainPage = new();

            Logger.Info("Start new test");
            Browser.NewBrowser.GoToUrl(TestSettings.ApplicationUrl);
            Waiters.WaitForPageLoad();
            Waiters.WaitForCondition(new Func<bool>(() => _mainPage.Cookies.Exists()));
            _mainPage.Cookies.AcceptAllCookiesButton.Click();
        }

        [TearDown]
        public virtual void CleanTest()
        {
            if (TestContext.CurrentContext.Result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Failed)
            {
                Logger.Info("Test is failed");
            }
            Logger.Info("Test finish");
            Browser.NewBrowser.Quit();
        }
    }
}