using NUnit.Framework;
using TAF.Core.Browser;
using TAF.Core.Utilities;
using TAF.Web.Pages;

namespace TAF.Tests.BDT.Steps
{
    [Binding]

    public class JobListingTestsSteps
    {
        private MainPage _mainPage;
        private JobListingsPage _jobListingsPage;
        private string defaultSearchMessage;

        [Given(@"setup page objects for job listing tests")]
        public void GivenSetupPageObjectsForJobListingTests()
        {
            _mainPage = new();
            _jobListingsPage = new();
        }

        [Given(@"I navigate to Careers button")]
        public void GivenINavigateToCareersButton()
        {
            Browser.NewBrowser.Action.MoveToElement(_mainPage.Header.CareersButton.OriginalWebElement).Build().Perform();
        }

        [Given(@"I click to Join Our Team button")]
        public void GivenIClickToJoinOurTeamButton()
        {
            Waiters.WaitForCondition(new Func<bool>(() => _mainPage.Header.JoinOurTeamOnCareersDropDown.IsDisplayed()));
            Browser.NewBrowser.Action.MoveToElement(_mainPage.Header.JoinOurTeamOnCareersDropDown.OriginalWebElement).Click().Build().Perform();
        }

        [When(@"I click to Open to Relocate button")]
        public void WhenIClickToOpenToRelocateButton()
        {
            defaultSearchMessage = _jobListingsPage.SearchResultQuantityMessage.GetText();
            _jobListingsPage.OpenToRelocateButton.Click();
        }

        [When(@"I scroll for more results")]
        public void WhenIScrollForMoreResults()
        {
            Waiters.WaitForCondition(() => _jobListingsPage.SearchResultQuantityMessage.GetText() != defaultSearchMessage);
            Browser.NewBrowser.ExecuteScript("window.scrollTo(0, document.body.scrollHeight);");
        }

        [Then(@"All (.*) vacancies contains Open to Relocate icon")]
        public void ThenAllVacanciesContainsOpenToRelocateIcon(int numberOfSearchResults)
        {
            Assert.That(_jobListingsPage.ListOfOpenToRelocateVacancies.GetElements().Count, Is.EqualTo(numberOfSearchResults), "Quantity of vacancies with possible relocation is less than quantity of search results");
        }
    }
}