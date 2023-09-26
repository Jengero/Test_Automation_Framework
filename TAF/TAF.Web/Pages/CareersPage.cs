using OpenQA.Selenium;
using TAF.Core.Elements;

namespace TAF.Web.Pages
{
    public class CareersPage : BasePage
    {
        public override string Url => "https://www.epam.com/careers";

        public Button FindYourDreamJobButton => new(By.XPath("//*[@class = 'arrow']//ancestor::*[contains (@href, 'job-listings') and @tabindex = '0']"));
    }
}