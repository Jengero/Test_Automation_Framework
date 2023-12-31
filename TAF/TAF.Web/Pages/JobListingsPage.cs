﻿using OpenQA.Selenium;
using OpenQA.Selenium.DevTools;
using TAF.Core.Elements;

namespace TAF.Web.Pages
{
    public class JobListingsPage : BasePage
    {
        public override string Url => "https://www.epam.com/careers/job-listings";

        public HtmlElement ThirteenthSearchResult => new(By.XPath("//*[@class = 'search-result__list']/*[contains (@class, 'item')][13]"));

        public HtmlElement ErrorSearchMessage => new(By.XPath("//*[contains(@class,'error-message-')]"));

        public HtmlElement FirstSearchResultDescription => new(By.XPath("//*[contains(@class,'description body-text')][1]"));

        public HtmlElement SkillsField => new(By.XPath("//*[@class='os-content' and contains(@style, 'auto')]"));

        public HtmlElement SearchResultQuantityMessage => new(By.XPath("//*[@class='search-result__heading-23 heading-1']"));

        public ElementsList<BaseElements> ListOfCountries => new(By.XPath("//*[@class = 'select2-results__group']"));

        public ElementsList<BaseElements> ListOfAvailableLocations => new(By.XPath("//*[contains (@class, 'js-tabs-title')]"));

        public ElementsList<BaseElements> ListOfSkills => new(By.XPath("//*[@class = 'checkbox-custom-label']"));

        public ElementsList<BaseElements> ListOfCities => new(By.XPath("//*[contains(@id, '-location-result-')]"));

        public ElementsList<BaseElements> ListOfOpenToRelocateVacancies => new(By.XPath("//*[@class= 'search-result__item']"));

        public TextInput KeywordJobInput => new(By.Id("new_form_job_search-keyword"));

        public TextInput LocationInput => new(By.XPath("//* [@class = 'select2-selection__rendered']"));

        public Button LocationsDropDownButton => new(By.XPath("//* [@class = 'select2-selection__arrow']"));

        public Button SubmitSearchButton => new(By.XPath("//button [@type = 'submit']"));

        public Button SkillsDropDownButton => new(By.XPath("//*[@class = 'default-label']"));

        public Button OpenToRelocateButton => new(By.XPath("//label [contains (@for, 'relocation')]"));
    }
}