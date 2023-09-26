using NUnit.Framework;
using TAF.Core.Browser;
using TAF.Core.Helpers;
//using TAF.Core.Helpers;
using TAF.Core.Utilities;
using TAF.Web.Pages;

namespace TAF.Tests.BDT.Steps
{
    [Binding]
    public class SearchTestsDefinitions
    {
        private MainPage _mainPage => new();
        private SearchResultsPage _searchResultsPage => new();
        private string searchQuery;
        private string firstSearchResultTitle;

        [Given(@"I click on Search field on Search Panel on Epam Landing page")]
        public void GivenIClickOnSearchFieldOnSearchPanelOnEpamLandingPage()
        {
            _mainPage.Header.SearchButton.Click();
        }

        [Given(@"I enter search query \(""([^""]*)""\)")]
        public void WhenIEnterSearchQuery(string query)
        {
            _mainPage.Header.SearchForm.SendKeys(query);
            searchQuery = query;
        }

        [When(@"I click on Find button on Search Panel on Epam Landing page")]
        public void WhenIClickOnFindButtonOnSearchPanelOnEpamLandingPage()
        {
            _mainPage.Header.FindResultButton.Click();
        }

        [Then(@"first (.*) search results contains search query in any place of the search results page")]
        public void ThenFirstSearchResultsContainsSearchQueryInAnyPlaceOfTheSearchResultsPage(int expectedNumberOfResults)
        {
            Assert.That(_searchResultsPage.ListOfArticles.GetElements().Take(expectedNumberOfResults).All(p => p.GetProperty("textContent").ToLower().Contains(searchQuery.ToLower())), Is.True, $"First {expectedNumberOfResults} articles don't contains search query");
        }

        [When(@"I click on the title of the first article on the search results page")]
        public void WhenIClickOnTheTitleOfTheFirstArticleOnTheSearchResultsPage()
        {
            Waiters.WaitForPageLoad();
            firstSearchResultTitle = _searchResultsPage.FirstArticle.GetAttribute("innerText");
            _searchResultsPage.FirstArticle.Click();
        }

        [Then(@"the title of the article coincide with first search result title on the article page")]
        public void ThenTheTitleOfTheArticleCoincideWithFirstSearchResultTitleOnTheArticlePage()
        {
            Assert.That(firstSearchResultTitle, Is.EqualTo(_searchResultsPage.FirstSearchResultTitle.GetProperty("innerText")), "Title of the first search result don't match with title of the first article");
        }

        [When(@"I scroll to the (.*) search result on the search results page")]
        public void WhenIScrollToTheThSearchResultOnTheSearchResultsPage(int numberOfSearchResult)
        {
            ActionsHelper.ScrollToElement(_searchResultsPage.ListOfArticles.GetElements()[numberOfSearchResult].OriginalWebElement).PerformAction();
        }

        [When(@"I scroll to the bottom of the search results page")]
        public void WhenIScrollToTheBottomOfTheSearchResultsPage()
        {
            Waiters.WaitForCondition(() => _searchResultsPage.ViewMoreButton.IsDisplayed());
            Browser.NewBrowser.ScrollToElement(_searchResultsPage.ViewMoreButton.OriginalWebElement);
        }

        [Then(@"the number of search results is (.*)")]
        public void ThenTheNumberOfSearchResultsIs(int expectedQuantityOfArticles)
        {
            Assert.That(_searchResultsPage.ListOfArticles.GetElements().Count, Is.EqualTo(expectedQuantityOfArticles), "The quantity of articles is not equal");
        }
    }
}