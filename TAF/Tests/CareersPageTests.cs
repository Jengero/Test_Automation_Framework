using OpenQA.Selenium;
using TAF.Core.Browser;
using TAF.Web.Pages;

namespace TAF.Tests
{
    public class CareersPageTests : BaseTest
    {
        private CareersPage _careersPage;
        private JobListingsPage _jobListingsPage;
        private MainPage _mainPage;
        private const string _availableCountriesLocator = "//*[contains (@class, 'js-tabs-title')]";

        [SetUp]
        public void SetUp() 
        {
            _mainPage = new();
            _careersPage = new();
            _jobListingsPage = new();
        }

        [Test]
        public void ListOfAvailableCountriesTest()
        {
            List<string> expectedCountries = new() { "AMERICAS", "EMEA", "APAC" };

            _mainPage.AcceptAllCookies();
            _mainPage.Header.CareersButton.Click();
            _careersPage.FindYourDreamJobButton.Click();
            Browser.NewBrowser.ScrollToElement(_jobListingsPage.thirteenthSearchResult.OriginalWebElement);
            var actualListOfCountries = _jobListingsPage.availableLocations.FindElements(By.XPath(_availableCountriesLocator)).Select(f => f.GetDomProperty("innerText"));

            CollectionAssert.AreEquivalent(expectedCountries, actualListOfCountries, "Countries are not equal");
        }
    }
}