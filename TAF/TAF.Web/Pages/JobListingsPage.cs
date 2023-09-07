using OpenQA.Selenium;
using TAF.Core.Elements;

namespace TAF.Web.Pages
{
    public class JobListingsPage : BasePage
    {
        public override string Url => "https://www.epam.com/careers/job-listings";

        public HtmlElement ThirteenthSearchResult => new(By.XPath("//*[@class = 'search-result__list']/*[contains (@class, 'item')][13]"));

        public ElementsList<BaseElements> ListOfAvailableLocations => new(By.XPath("//*[contains (@class, 'js-tabs-title')]"));
    }
}