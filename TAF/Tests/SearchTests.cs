using System.Drawing;
using TAF.Core.Browser;
using TAF.Core.Elements;
using TAF.Core.Utilities;
using TAF.Helper;
using TAF.Utilities.Parser;
using TAF.Web.Pages;
using TestData;

namespace TAF.Tests
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    public class SearchTests : BaseTest
    {
        private MainPage _mainPage;
        private SearchResultsPage _searchResultsPage;
        private JobListingsPage _jobListingsPage;
        string defaultSearchText = "We Found 2335 Job Openings for You";

        [SetUp]
        public void SetUp()
        {
            _mainPage = new();
            _searchResultsPage = new();
            _jobListingsPage = new();
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

            Assert.That(_searchResultsPage.ListOfArticles.GetElements().Take(expectedNumberOfResults).All(p => p.GetProperty("textContent").ToLower().Contains(searchQuery.ToLower())), Is.True, "First five articles don't contains search query");
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

        [Test]
        [TestCaseSource(nameof(GetKeywordData))]
        public void KeywordQueryJobListingsPageTest(SearchModel searchModel)
        {
            Browser.NewBrowser.Action.MoveToElement(_mainPage.Header.CareersButton.OriginalWebElement).Build().Perform();

            Waiters.WaitForCondition(new Func<bool>(() => _mainPage.Header.JoinOurTeamOnCareersDropDown.IsDisplayed()));

            Browser.NewBrowser.Action.MoveToElement(_mainPage.Header.JoinOurTeamOnCareersDropDown.OriginalWebElement).Click().Build().Perform();
            _jobListingsPage.KeywordJobInput.SendKeys(searchModel.KeywordOrJobId);
            _jobListingsPage.SubmitSearchButton.Click();
            Waiters.WaitForCondition(() => _jobListingsPage.ResultDescription.GetProperty("innerText") != defaultSearchText);

            Assert.That(_jobListingsPage.FirstSearchResultDescription.GetText().ToLower(), Is.EqualTo(searchModel.SearchResult.ToLower()), "Descriptions of the first result don't match (keyword)");
        }

        [Test]
        [TestCaseSource(nameof(GetLocationData))]
        public void LocationFieldJobListingsPageTest(SearchModel searchModel)
        {
            Browser.NewBrowser.Action.MoveToElement(_mainPage.Header.CareersButton.OriginalWebElement).Build().Perform();

            Waiters.WaitForCondition(new Func<bool>(() => _mainPage.Header.JoinOurTeamOnCareersDropDown.IsDisplayed()));

            Browser.NewBrowser.Action.MoveToElement(_mainPage.Header.JoinOurTeamOnCareersDropDown.OriginalWebElement).Click().Build().Perform();
            _jobListingsPage.LocationsDropDownButton.Click();
            _jobListingsPage.ListOfCountries.GetElements().FirstOrDefault(f => f.GetText().Equals(searchModel.LocationCountry)).Click();
            _jobListingsPage.ListOfCities.GetElements().FirstOrDefault(f => f.GetText().Equals(searchModel.LocationCity)).Click();
            Waiters.WaitForCondition(() => _jobListingsPage.ResultDescription.GetProperty("innerText") != defaultSearchText);

            Assert.That(_jobListingsPage.FirstSearchResultDescription.GetText().ToLower(), Is.EqualTo(searchModel.SearchResult.ToLower()), "Descriptions of the first result don't match (location)");
        }

        [Test]
        [TestCaseSource(nameof(GetSkillsData))]
        public void SkillsFieldJobListingsPageTest(SearchModel searchModel)
        {
            Browser.NewBrowser.Action.MoveToElement(_mainPage.Header.CareersButton.OriginalWebElement).Build().Perform();

            Waiters.WaitForCondition(new Func<bool>(() => _mainPage.Header.JoinOurTeamOnCareersDropDown.IsDisplayed()));

            Browser.NewBrowser.Action.MoveToElement(_mainPage.Header.JoinOurTeamOnCareersDropDown.OriginalWebElement).Click().Build().Perform();
            _jobListingsPage.SkillsDropDownButton.Click();
            Waiters.WaitForCondition(() => _jobListingsPage.SkillsFieldUp.OriginalWebElement.Displayed);
            _jobListingsPage.ListOfSkills.GetElements().FirstOrDefault(f => f.GetText().Equals(searchModel.Skills)).Click();
            Waiters.WaitForCondition(() => _jobListingsPage.ResultDescription.GetProperty("innerText") != defaultSearchText);

            Assert.That(_jobListingsPage.FirstSearchResultDescription.GetText().ToLower(), Is.EqualTo(searchModel.SearchResult.ToLower()), "Descriptions of the first result don't match (skills)");
        }

        [Test]
        [TestCaseSource(nameof(GetAllFieldsData))]
        public void SearchQueryWithAllFieldsFilledInJobListingsPageTest(SearchModel searchModel )
        {
            Browser.NewBrowser.Action.MoveToElement(_mainPage.Header.CareersButton.OriginalWebElement).Build().Perform();

            Waiters.WaitForCondition(new Func<bool>(() => _mainPage.Header.JoinOurTeamOnCareersDropDown.IsDisplayed()));

            Browser.NewBrowser.Action.MoveToElement(_mainPage.Header.JoinOurTeamOnCareersDropDown.OriginalWebElement).Click().Build().Perform();

            _jobListingsPage.KeywordJobInput.SendKeys(searchModel.KeywordOrJobId);

            _jobListingsPage.LocationsDropDownButton.Click();
            _jobListingsPage.ListOfCountries.GetElements().FirstOrDefault(f => f.GetText().Equals(searchModel.LocationCountry)).Click();
            _jobListingsPage.ListOfCities.GetElements().FirstOrDefault(f => f.GetText().Equals(searchModel.LocationCity)).Click();
            _jobListingsPage.SkillsDropDownButton.Click();
            Waiters.WaitForCondition(() => _jobListingsPage.SkillsFieldDown.OriginalWebElement.Displayed);
            _jobListingsPage.ListOfSkills.GetElements().FirstOrDefault(f => f.GetText().Equals(searchModel.Skills)).Click();
            _jobListingsPage.SubmitSearchButton.Click();
            Waiters.WaitForCondition(() => _jobListingsPage.ResultDescription.GetProperty("innerText") != defaultSearchText);

            Assert.That(_jobListingsPage.FirstSearchResultDescription.GetText().ToLower(), Is.EqualTo(searchModel.SearchResult.ToLower()), "Descriptions of the first result don't match (all fields)");
        }

        [Test]
        [TestCaseSource(nameof(GetUnsuccessfullQueryData))]
        public void UnsuccessfulSearchJobListingsPageTest(SearchModel searchModel)
        {
            Browser.NewBrowser.Action.MoveToElement(_mainPage.Header.CareersButton.OriginalWebElement).Build().Perform();

            Waiters.WaitForCondition(new Func<bool>(() => _mainPage.Header.JoinOurTeamOnCareersDropDown.IsDisplayed()));

            Browser.NewBrowser.Action.MoveToElement(_mainPage.Header.JoinOurTeamOnCareersDropDown.OriginalWebElement).Click().Build().Perform();

            _jobListingsPage.KeywordJobInput.SendKeys(searchModel.KeywordOrJobId);
            _jobListingsPage.LocationsDropDownButton.Click();
            _jobListingsPage.ListOfCountries.GetElements().FirstOrDefault(f => f.GetText().Equals(searchModel.LocationCountry)).Click();
            _jobListingsPage.ListOfCities.GetElements().FirstOrDefault(f => f.GetText().Equals(searchModel.LocationCity)).Click();
            _jobListingsPage.SubmitSearchButton.Click();
            Waiters.WaitForCondition(() => _jobListingsPage.ResultDescription.GetProperty("innerText") != defaultSearchText);

            Assert.That(_jobListingsPage.ErrorSearchMessage.GetText().ToLower(), Is.EqualTo(searchModel.SearchResult.ToLower()), "Incorrect error message");
        }

        private static List<SearchModel> GetKeywordData()
        {
            var json = File.ReadAllText(@"C:\Users\Jenger\Desktop\Mems\Test_Automation_Framework\TAF\TestData\TestDataFiles\KeywordQueryJobListingsPageTest.json");

            return JsonParser.DeserializeJsonToObjects<SearchModel>(json);
        }

        private static List<SearchModel> GetLocationData()
        {
            var json = File.ReadAllText(@"C:\Users\Jenger\Desktop\Mems\Test_Automation_Framework\TAF\TestData\TestDataFiles\LocationFieldJobListingsPageTest.json");

            return JsonParser.DeserializeJsonToObjects<SearchModel>(json);
        }

        private static List<SearchModel> GetSkillsData()
        {
            var json = File.ReadAllText(@"C:\Users\Jenger\Desktop\Mems\Test_Automation_Framework\TAF\TestData\TestDataFiles\SkillsFieldJobListingsPageTest.json");

            return JsonParser.DeserializeJsonToObjects<SearchModel>(json);
        }

        private static List<SearchModel> GetAllFieldsData()
        {
            var json = File.ReadAllText(@"C:\Users\Jenger\Desktop\Mems\Test_Automation_Framework\TAF\TestData\TestDataFiles\SearchQueryWithAllFieldsFilledInJobListingsPageTest.json");

            return JsonParser.DeserializeJsonToObjects<SearchModel>(json);
        }

        private static List<SearchModel> GetUnsuccessfullQueryData()
        {
            var json = File.ReadAllText(@"C:\Users\Jenger\Desktop\Mems\Test_Automation_Framework\TAF\TestData\TestDataFiles\UnsuccessfulSearchJobListingsPageTest.json");

            return JsonParser.DeserializeJsonToObjects<SearchModel>(json);
        }
    }
}