using NUnit.Framework;
using TAF.Core.Browser;
using TAF.Core.Utilities;
using TAF.Web.Pages;

namespace TAF.Tests.BDT.Steps
{
    [Binding]
    public class SearchTestsDefinitions
    {
        private MainPage _mainPage;
        private SearchResultsPage _searchResultsPage;
        private string searchQuery;
        private string firstSearchResultTitle;

        [Given(@"setup page objects for search tests")]
        public void GivenSetupPageObjectsForSearchTests()
        {
            _mainPage = new();
            _searchResultsPage = new();
        }

        [Given(@"I click to search field")]
        public void WhenIClickToSearchField()
        {
            _mainPage.Header.SearchButton.Click();
        }

        [Given(@"I enter search query \(""([^""]*)""\)")]
        public void WhenIEnterSearchQuery(string query)
        {
            _mainPage.Header.SearchForm.SendKeys(query);
            searchQuery = query;
        }

        [When(@"I click find result button")]
        public void WhenIClickFindResultButton()
        {
            _mainPage.Header.FindResultButton.Click();
        }

        [Then(@"first (.*) search results contains search query in any place")]
        public void ThenFirstSearchResultsContainsSearchQueryInAnyPlace(int expectedNumberOfResults)
        {
            Assert.That(_searchResultsPage.ListOfArticles.GetElements().Take(expectedNumberOfResults).All(p => p.GetProperty("textContent").ToLower().Contains(searchQuery.ToLower())), Is.True, "First five articles don't contains search query");
        }

        [When(@"I click to the title of the first article")]
        public void WhenIClickToTheTitleOfTheFirstArticle()
        {
            Waiters.WaitForPageLoad();
            firstSearchResultTitle = _searchResultsPage.FirstArticle.GetAttribute("innerText");
            _searchResultsPage.FirstArticle.Click();
        }

        [Then(@"the title of the article coincide with first search result title")]
        public void ThenTheTitleOfTheArticleCoincideWithFirstSearchResultTitle()
        {
            Assert.That(firstSearchResultTitle, Is.EqualTo(_searchResultsPage.FirstSearchResultTitle.GetProperty("innerText")), "Title of the first search result don't match with title of the first article");
        }

        [When(@"I scroll to the (.*) search result")]
        public void WhenIScrollToTheThSearchResult(int numberOfSearchResult)
        {
            Browser.NewBrowser.ScrollToElement(_searchResultsPage.ListOfArticles.GetElements()[numberOfSearchResult].OriginalWebElement);
        }

        [When(@"I scroll to the bottom of the page")]
        public void WhenIScrollToTheBottomOfThePage()
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