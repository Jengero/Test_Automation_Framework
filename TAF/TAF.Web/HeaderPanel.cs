using OpenQA.Selenium;
using TAF.Core.Elements;

namespace TAF.Web
{
    public class HeaderPanel : BasePanel
    {
        public HeaderPanel HeaderSearchPanel => new(By.ClassName("//*[@class = 'header__content']"));

        public Button SearchButton => new(By.XPath("//span [contains(@class, 'search-icon')]"));

        public Button LanguageSelectorButton => new(By.XPath("//*[@class = 'location-selector__button']"));

        public Button CareersButton => new(By.XPath("//*[contains(@class, 'item-link') and @href = '/careers']"));

        public Button JoinOurTeamOnCareersDropDown => new(By.XPath("//a[contains(@class, 'top') and contains(@href, 'job')]")); 

        public Button FindResultButton => new(By.XPath("//span [contains (@class, 'bth')]"));

        public TextInput SearchForm => new(By.XPath("//*[@id = 'new_form_search']"));

        public HtmlElement LocationPanel => new(By.ClassName("location-selector__panel"));

        public ElementsList<BaseElements> ListOfLocalizations => new(By.XPath("//*[@class = 'location-selector__item']"));

        public HeaderPanel(By locator): base(locator) { }
    }
}