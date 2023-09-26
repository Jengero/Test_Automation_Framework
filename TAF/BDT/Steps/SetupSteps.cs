using NUnit.Framework;
using TAF.Core.Browser;
using TAF.Core.Utilities;
using TAF.Helper;
using TAF.Utilities.Logger;

namespace TAF.Tests.BDT.Steps
{
    [Binding]
    public class SetupSteps
    {

        [BeforeScenarioBlock]
        public void LoggerSetup() => Logger.NewLogger(TestContext.CurrentContext.Test.Name, TestContext.CurrentContext.TestDirectory);

        [Given(@"navigation to EPAM Landing page")]
        public void GivenNavigationToEPAMLandingPage()
        {
            Logger.Info("Start new test");
            Browser.NewBrowser.GoToUrl(TestSettings.ApplicationUrl);
            Waiters.WaitForPageLoad();
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