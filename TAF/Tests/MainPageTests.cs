using OpenQA.Selenium.Interactions;
using TAF.Core.Browser;
using TAF.Core.Utilities;
using TAF.Web.Pages;

namespace TAF.Tests
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    public class MainPageTests : BaseTest
    {
        private MainPage _mainPage;
        private CareersPage _careersPage;
        private ClientWorkPage _workPage;
        private JobListingsPage _jobListingsPage;
        private Actions _actions;

        [SetUp]
        public void SetUp()
        {
            _mainPage = new();
            _careersPage = new();
            _workPage = new();
            _jobListingsPage = new();
            _actions = new(Browser.NewBrowser.Driver);
            _mainPage.AcceptAllCookies();
        }
        
        [Test]
        public void MainPageAvailabilityTest()
        {
            Assert.That(_mainPage.IsOpened(), "Main Page is unavailable");
        }
        
        [Test]
        public void PageTransitionTest()
        {
            Browser.NewBrowser.GoToUrl(_careersPage.Url);
            Browser.NewBrowser.GoToUrl(_workPage.Url);
            Browser.NewBrowser.Refresh();
            Browser.NewBrowser.Back();

            Assert.That(Browser.NewBrowser.Url, Is.EqualTo(_careersPage.Url), "Pages don't match");
        }
        
        [Test]
        public void JoinOutTeemButtonOnCareersDropDownHeaderTest()
        {
            _actions.MoveToElement(_mainPage.Header.CareersButton.OriginalWebElement).MoveToElement(_mainPage.Header.JoinOurTeamOnCareersDropDown.OriginalWebElement).Click().Build().Perform();
            
            Assert.That(Browser.NewBrowser.Url, Is.EqualTo(_jobListingsPage.Url), "Loaded incorrect page");
        }
        
        [Test]
        public void ListOfLocalizationsOnHeaderTest()
        {
            List<string> expectedLocalizations = new() { "Global (English)", "Hungary (English)", "СНГ (Русский)", "Česká Republika (Čeština)", "India (English)", "Україна (Українська)", "Czech Republic (English)", "日本 (日本語)", "中国 (中文)", "DACH (Deutsch)", "Polska (Polski)" };
            
            _mainPage.Header.LanguageSelectorButton.Click();

            Waiters.WaitForCondition(new Func<bool>(() => _mainPage.Header.LocationPanel.IsDisplayed()));

            Assert.That(_mainPage.Header.ListOfLocalizations.Select(a => a.GetProperty("innerText").ToList()), Is.EqualTo(expectedLocalizations), "Represented localizations are not equal");
        }
    }
}