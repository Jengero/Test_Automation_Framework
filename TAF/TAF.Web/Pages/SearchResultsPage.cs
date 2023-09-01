using OpenQA.Selenium;
using TAF.Core.Elements;

namespace TAF.Web.Pages
{
    public class SearchResultsPage : BasePage
    {
        public override string Url => "https://www.epam.com/search";

        public TextInput SearchFormOnResultPage => new(By.XPath("//input[contains (@aria-describedby, 'results')]"));

        public HtmlElement SearchResultsElement => new(By.XPath("//*[@class = 'search-results__items']"));

        public ElementsList <BaseElements> ListOfArticles => new(By.XPath("//div[@class = 'search-results__items']/article"));

        public HtmlElement FirstArticle => new(By.XPath("//* [@class = 'search-results__title'][1]"));

        public Label FirstSearchResultTitle => new(By.XPath("//*[contains(@class, '80')]/*[contains(@class, 'light')]"));
        
        public Button ViewMoreButton => new(By.XPath("//span [contains (@class,  'more')]"));
    }
}