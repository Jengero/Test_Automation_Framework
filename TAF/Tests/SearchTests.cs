using TAF.Core.Browser;
using TAF.Core.Elements;
using TAF.Core.Utilities;
using TAF.Web.Pages;

namespace TAF.Tests
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    public class SearchTests : BaseTest
    {
        private MainPage _mainPage;
        private SearchResultsPage _searchResultsPage;

        [SetUp]
        public void SetUp()
        {
            _mainPage = new();
            _searchResultsPage = new();
        }


        [TestCase("Business Analysis")]
        public void SearchPanelResultIsCorrectTest(string expectedSearchResult)
        {
            _mainPage.Header.SearchButton.Click();
            _mainPage.Header.SearchForm.SendKeys(expectedSearchResult);
            _mainPage.Header.FindResultButton.Click();

            Waiters.WaitForCondition(new Func<bool>(() => _searchResultsPage.SearchFormOnResultPage.IsDisplayed()));

            Assert.That(_searchResultsPage.SearchFormOnResultPage.GetProperty("value"), Is.EqualTo(expectedSearchResult), "Search results don't match");
        }

        [TestCase(5, "Automation")]
        public void EnteredTextIsPresentInSearchResultsTest(int expectedNumberOfResults, string searchQuery)
        {
            _mainPage.Header.SearchButton.Click();
            _mainPage.Header.SearchForm.SendKeys(searchQuery);
            _mainPage.Header.FindResultButton.Click();

            Assert.That(_searchResultsPage.ListOfArticles.GetElements().Take(expectedNumberOfResults).All(p => p.GetProperty("textContent").ToLower().Contains(searchQuery.ToLower())), Is.True, "First five articles don't contains search query" );
        }

        
        [TestCase("Business Analysis")]
        public void ArticleTitleMatchTest(string expectedSearchResult)
        {
            _mainPage.Header.SearchButton.Click();
            _mainPage.Header.SearchForm.SendKeys(expectedSearchResult);
            _mainPage.Header.FindResultButton.Click();

            Waiters.WaitForCondition(new Func<bool>(() => _searchResultsPage.FirstArticle.IsDisplayed()));

            var firstSearchResultTitle = _searchResultsPage.FirstArticle.GetAttribute("innerText");
            _searchResultsPage.FirstArticle.Click();

            Assert.That(firstSearchResultTitle, Is.EqualTo(_searchResultsPage.FirstSearchResultTitle.GetProperty("innerText")), "Title of the first search result don't match with title of the first article");
        }

        
        [TestCase("Automation", "20")]
        public void QuantityOfSearchResultsTest(string searchQuery, string expectedQuantityOfArticles)
        {
            _mainPage.Header.SearchButton.ClickUsingJS();
            _mainPage.Header.SearchForm.SendKeys(searchQuery);
            _mainPage.Header.FindResultButton.Click();

            Browser.NewBrowser.ScrollToElement(_searchResultsPage.ListOfArticles.GetElements()[9].OriginalWebElement);

            Waiters.WaitForCondition(() => _searchResultsPage.ViewMoreButton.IsDisplayed());

            Browser.NewBrowser.ScrollToElement(_searchResultsPage.ViewMoreButton.OriginalWebElement);

            Assert.That(_searchResultsPage.ListOfArticles.GetElements().Count.ToString(), Is.EqualTo(expectedQuantityOfArticles), "The quantity of articles is not equal");
        }
    }
}