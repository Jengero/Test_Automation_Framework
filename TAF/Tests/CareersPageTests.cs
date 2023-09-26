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

            _mainPage.Header.CareersButton.Click();
            _careersPage.FindYourDreamJobButton.Click();
            Browser.NewBrowser.ScrollToElement(_jobListingsPage.ThirteenthSearchResult.OriginalWebElement);

            CollectionAssert.AreEquivalent(expectedCountries, _jobListingsPage.ListOfAvailableLocations.Select(f => f.GetProperty("innerText")), "Countries are not equal");
        }
    }
}