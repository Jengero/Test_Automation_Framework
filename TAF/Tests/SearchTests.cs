using TAF.Core.Browser;
using TAF.Core.Elements;
using TAF.Core.Helpers;
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
        private const string _keywordOrJob = "\\KeywordQueryJobListingsPageTest.json";
        private const string _location = "\\LocationFieldJobListingsPageTest.json";
        private const string _skills = "\\SkillsFieldJobListingsPageTest.json";
        private const string _allFields = "\\SearchQueryWithAllFieldsFilledInJobListingsPageTest.json";
        private const string _unsuccessfulQuery = "\\UnsuccessfulSearchJobListingsPageTest.json";

        [SetUp]
        public void SetUp()
        {
            _mainPage = new();
            _searchResultsPage = new();
            _jobListingsPage = new();
        }

        [NonParallelizable]
        [TestCase("Business Analysis")]
        public void SearchPanelResultIsCorrectTest(string expectedSearchResult)
        {
            _mainPage.Header.SearchButton.Click();
            _mainPage.Header.SearchForm.SendKeys(expectedSearchResult);
            _mainPage.Header.FindResultButton.Click();

            Waiters.WaitForCondition(new Func<bool>(() => _searchResultsPage.SearchFormOnResultPage.IsDisplayed()));

            Assert.That(_searchResultsPage.SearchFormOnResultPage.GetProperty("value"), Is.EqualTo(expectedSearchResult), "Search results don't match");
        }

        [NonParallelizable]
        [TestCase(5, "Automation")]
        public void EnteredTextIsPresentInSearchResultsTest(int expectedNumberOfResults, string searchQuery)
        {
            _mainPage.Header.SearchButton.Click();
            _mainPage.Header.SearchForm.SendKeys(searchQuery);
            _mainPage.Header.FindResultButton.Click();

            Assert.That(_searchResultsPage.ListOfArticles.GetElements().Take(expectedNumberOfResults).All(p => p.GetProperty("textContent").ToLower().Contains(searchQuery.ToLower())), Is.True, "First five articles don't contains search query");
        }

        [NonParallelizable]
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

        [NonParallelizable]
        [TestCase("Automation", "20")]
        public void QuantityOfSearchResultsTest(string searchQuery, string expectedQuantityOfArticles)
        {
            _mainPage.Header.SearchButton.ClickUsingJS();
            _mainPage.Header.SearchForm.SendKeys(searchQuery);
            _mainPage.Header.FindResultButton.Click();

            Browser.NewBrowser.ScrollToElement(_searchResultsPage.ListOfArticles.GetElements().Last().OriginalWebElement);

            Waiters.WaitForCondition(() => _searchResultsPage.ViewMoreButton.IsDisplayed());

            Browser.NewBrowser.ScrollToElement(_searchResultsPage.ViewMoreButton.OriginalWebElement);

            Assert.That(_searchResultsPage.ListOfArticles.GetElements().Count.ToString(), Is.EqualTo(expectedQuantityOfArticles), "The quantity of articles is not equal");
        }

        [Test]
        [TestCaseSource(nameof(GetKeywordData))]
        public void KeywordQueryJobListingsPageTest(SearchModel searchModel)
        {
            ActionsHelper.MoveToElement(_mainPage.Header.CareersButton.OriginalWebElement).PerformAction();
            Waiters.WaitForCondition(new Func<bool>(() => _mainPage.Header.JoinOurTeamOnCareersDropDown.IsDisplayed()));
            ActionsHelper.MoveToElement(_mainPage.Header.JoinOurTeamOnCareersDropDown.OriginalWebElement).Click().PerformAction();

            var defaultSearchMessage = _jobListingsPage.SearchResultQuantityMessage.GetText();

            _jobListingsPage.KeywordJobInput.SendKeys(searchModel.KeywordOrJobId);
            _jobListingsPage.SubmitSearchButton.Click();
            Waiters.WaitForCondition(() => _jobListingsPage.SearchResultQuantityMessage.GetText() != defaultSearchMessage);

            Assert.That(_jobListingsPage.FirstSearchResultDescription.GetText().ToLower(), Is.EqualTo(searchModel.SearchResult.ToLower()), "Descriptions of the first result don't match (keyword)");
        }

        [NonParallelizable]
        [Test]
        [TestCaseSource(nameof(GetLocationData))]
        public void LocationFieldJobListingsPageTest(SearchModel searchModel)
        {
            ActionsHelper.MoveToElement(_mainPage.Header.CareersButton.OriginalWebElement).PerformAction();
            Waiters.WaitForCondition(new Func<bool>(() => _mainPage.Header.JoinOurTeamOnCareersDropDown.IsDisplayed()));
            ActionsHelper.MoveToElement(_mainPage.Header.JoinOurTeamOnCareersDropDown.OriginalWebElement).Click().PerformAction();

            var defaultSearchMessage = _jobListingsPage.SearchResultQuantityMessage.GetText();

            _jobListingsPage.LocationsDropDownButton.Click();
            _jobListingsPage.ListOfCountries.GetElements().FirstOrDefault(f => f.GetText().Equals(searchModel.LocationCountry)).Click();
            _jobListingsPage.ListOfCities.GetElements().FirstOrDefault(f => f.GetText().Equals(searchModel.LocationCity)).Click();
            Waiters.WaitForCondition(() => _jobListingsPage.SearchResultQuantityMessage.GetText() != defaultSearchMessage);

            Assert.That(_jobListingsPage.FirstSearchResultDescription.GetText().ToLower(), Is.EqualTo(searchModel.SearchResult.ToLower()), "Descriptions of the first result don't match (location)");
        }

        [Test]
        [TestCaseSource(nameof(GetSkillsData))]
        public void SkillsFieldJobListingsPageTest(SearchModel searchModel)
        {
            ActionsHelper.MoveToElement(_mainPage.Header.CareersButton.OriginalWebElement).PerformAction();
            Waiters.WaitForCondition(new Func<bool>(() => _mainPage.Header.JoinOurTeamOnCareersDropDown.IsDisplayed()));
            ActionsHelper.MoveToElement(_mainPage.Header.JoinOurTeamOnCareersDropDown.OriginalWebElement).Click().PerformAction();

            var defaultSearchMessage = _jobListingsPage.SearchResultQuantityMessage.GetText();

            _jobListingsPage.SkillsDropDownButton.Click();
            Waiters.WaitForCondition(() => _jobListingsPage.SkillsField.OriginalWebElement.Displayed);
            _jobListingsPage.ListOfSkills.GetElements().FirstOrDefault(f => f.GetText().Equals(searchModel.Skills)).Click();
            Waiters.WaitForCondition(() => _jobListingsPage.SearchResultQuantityMessage.GetText() != defaultSearchMessage);

            Assert.That(_jobListingsPage.FirstSearchResultDescription.GetText().ToLower(), Is.EqualTo(searchModel.SearchResult.ToLower()), "Descriptions of the first result don't match (skills)");
        }
        
        [Test]
        [TestCaseSource(nameof(GetAllFieldsData))]
        public void SearchQueryWithAllFieldsFilledInJobListingsPageTest(SearchModel searchModel )
        {
            ActionsHelper.MoveToElement(_mainPage.Header.CareersButton.OriginalWebElement).PerformAction();
            Waiters.WaitForCondition(new Func<bool>(() => _mainPage.Header.JoinOurTeamOnCareersDropDown.IsDisplayed()));
            ActionsHelper.MoveToElement(_mainPage.Header.JoinOurTeamOnCareersDropDown.OriginalWebElement).Click().PerformAction();

            var defaultSearchMessage = _jobListingsPage.SearchResultQuantityMessage.GetText();

            _jobListingsPage.KeywordJobInput.SendKeys(searchModel.KeywordOrJobId);
            _jobListingsPage.LocationsDropDownButton.Click();
            _jobListingsPage.ListOfCountries.GetElements().FirstOrDefault(f => f.GetText().Equals(searchModel.LocationCountry)).Click();
            _jobListingsPage.ListOfCities.GetElements().FirstOrDefault(f => f.GetText().Equals(searchModel.LocationCity)).Click();
            _jobListingsPage.SkillsDropDownButton.Click();

            Waiters.WaitForCondition(() => _jobListingsPage.SkillsField.OriginalWebElement.Displayed);

            _jobListingsPage.ListOfSkills.GetElements().FirstOrDefault(f => f.GetText().Equals(searchModel.Skills)).Click();
            _jobListingsPage.SubmitSearchButton.Click();
            Waiters.WaitForCondition(() => _jobListingsPage.SearchResultQuantityMessage.GetText() != defaultSearchMessage);


            Assert.That(_jobListingsPage.FirstSearchResultDescription.GetText().ToLower(), Is.EqualTo(searchModel.SearchResult.ToLower()), "Descriptions of the first result don't match (all fields)");
        }
        
        [Test]
        [TestCaseSource(nameof(GetUnsuccessfullQueryData))]
        public void UnsuccessfulSearchJobListingsPageTest(SearchModel searchModel)
        {
            ActionsHelper.MoveToElement(_mainPage.Header.CareersButton.OriginalWebElement).PerformAction();
            Waiters.WaitForCondition(new Func<bool>(() => _mainPage.Header.JoinOurTeamOnCareersDropDown.IsDisplayed()));
            ActionsHelper.MoveToElement(_mainPage.Header.JoinOurTeamOnCareersDropDown.OriginalWebElement).Click().PerformAction();

            var defaultSearchMessage = _jobListingsPage.SearchResultQuantityMessage.GetText();

            _jobListingsPage.KeywordJobInput.SendKeys(searchModel.KeywordOrJobId);
            _jobListingsPage.LocationsDropDownButton.Click();
            _jobListingsPage.ListOfCountries.GetElements().FirstOrDefault(f => f.GetText().Equals(searchModel.LocationCountry)).Click();
            _jobListingsPage.ListOfCities.GetElements().FirstOrDefault(f => f.GetText().Equals(searchModel.LocationCity)).Click();
            _jobListingsPage.SubmitSearchButton.Click();
            Waiters.WaitForCondition(() => _jobListingsPage.SearchResultQuantityMessage.GetText() != defaultSearchMessage);

            Assert.That(_jobListingsPage.ErrorSearchMessage.GetText().ToLower(), Is.EqualTo(searchModel.SearchResult.ToLower()), "Incorrect error message");
        }

        private static List<SearchModel> GetKeywordData()
        {
            var json = File.ReadAllText(TestSettings.DataDirPath + _keywordOrJob);

            return JsonParser.DeserializeJsonToObjects<SearchModel>(json);
        }

        private static List<SearchModel> GetLocationData()
        {
            var json = File.ReadAllText(TestSettings.DataDirPath + _location);

            return JsonParser.DeserializeJsonToObjects<SearchModel>(json);
        }

        private static List<SearchModel> GetSkillsData()
        {
            var json = File.ReadAllText(TestSettings.DataDirPath + _skills);

            return JsonParser.DeserializeJsonToObjects<SearchModel>(json);
        }

        private static List<SearchModel> GetAllFieldsData()
        {
            var json = File.ReadAllText(TestSettings.DataDirPath + _allFields);

            return JsonParser.DeserializeJsonToObjects<SearchModel>(json);
        }

        private static List<SearchModel> GetUnsuccessfullQueryData()
        {
            var json = File.ReadAllText(TestSettings.DataDirPath + _unsuccessfulQuery);

            return JsonParser.DeserializeJsonToObjects<SearchModel>(json);
        }
    }
}