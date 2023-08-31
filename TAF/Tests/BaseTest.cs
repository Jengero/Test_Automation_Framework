using TAF.Core.Browser;
using TAF.Core.Utilities;
using TAF.Helper;
using TAF.Utilities.Logger;

namespace TAF.Tests
{
    public abstract class BaseTest
    {
        public TestContext TestContext { get; set; }

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Logger.NewLogger(TestContext.CurrentContext.Test.Name, TestContext.CurrentContext.TestDirectory);

        }

        [SetUp]
        public virtual void BeforeTest()
        {
            Logger.Info("Start new test");
            Browser.NewBrowser.GoToUrl(TestSettings.ApplicationUrl);
            Waiters.WaitForPageLoad();
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