using NUnit.Framework;
using TAF.Core.Browser;
using TAF.Core.Helpers;
using TAF.Core.Utilities;
using TAF.Web.Pages;

namespace TAF.Tests.BDT.Steps
{
    [Binding]
    public class JobListingTestsSteps
    {
        private MainPage _mainPage => new();
        private JobListingsPage _jobListingsPage => new();
        private string defaultSearchMessage;

        [Given(@"I open Careers dropdown on Epam Landing page")]
        public void GivenIOpenCareersDropdownOnEpamLandingPage()
        {
            ActionsHelper.MoveToElement(_mainPage.Header.CareersButton.OriginalWebElement).PerformAction();
        }

        [Given(@"I click on Join Our Team button on Careers dropdown on Epam Landing page")]
        public void GivenIClickOnJoinOurTeamButtonOnCareersDropdownOnEpamLandingPage()
        {
            Waiters.WaitForCondition(new Func<bool>(() => _mainPage.Header.JoinOurTeamOnCareersDropDown.IsDisplayed()));
            ActionsHelper.MoveToElement(_mainPage.Header.JoinOurTeamOnCareersDropDown.OriginalWebElement).Click().PerformAction();
        }

        [When(@"I click on Open to Relocate checkbox on Join our Team page")]
        public void WhenIClickOnOpenToRelocateCheckboxOnJoinOurTeamPage()
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