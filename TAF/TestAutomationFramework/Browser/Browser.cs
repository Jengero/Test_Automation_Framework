﻿using System.Collections.ObjectModel;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using TAF.Helper;
using TAF.Utilities.Logger;

namespace TAF.Core.Browser
{
    public class Browser
    {
        private readonly IWebDriver _driver;
        private static ThreadLocal<Browser> _browser;

        public static Browser NewBrowser
        {
            get
            {
                _browser.Value = _browser.Value != null
                    ? _browser.Value
                    : new Browser(DriverFactory.GetWebDriver(TestSettings.Browser));
                return _browser.Value;
            }
        }

        static Browser()
        {
            _browser = new ThreadLocal<Browser>(() => new Browser(DriverFactory.GetWebDriver(TestSettings.Browser)));
        }

        public Browser(IWebDriver driver)
        {
            _driver = driver;
        }

        public static void CloseBrowser()
        {
            Logger.Info("Quit browser");
            _browser.Value.Quit();
            _browser.Value = null;
        }

        #region Driver methods
        public void Back()
        {
            Logger.Info("Navigate Back");
            _driver.Navigate().Back();
        }

        public string GetUrl()
        {
            Logger.Info("Return current Url");
            return _driver.Url;
        }

        public void Refresh()
        {
            Logger.Info("Refresh page");
            _driver.Navigate().Refresh();
        }

        public void GoToUrl(string url)
        {
            Logger.Info($"Open url: {url}");
            _driver.Navigate().GoToUrl(url);
        }

        public void ScrollTop()
        {
            Logger.Info("Scroll page top");
            ExecuteScript("$(window).scrollTop(0)");
        }

        public void ScrollToElement(IWebElement element)
        {
            ExecuteScript("arguments[0].scrollIntoView(true);", element);
        }

        public void Close()
        {
            Logger.Info("Close current page");
            _driver.Close();
        }

        public void Quit()
        {
            Logger.Info("Quit Browser");
            try
            {
                _driver.Quit();
            }

            catch (Exception ex)
            {
                Logger.Info($"Unable to Quit the browser. Reason: {ex.Message}");
            }
        }

        public IWebElement FindElement(By locator)
        {
            return _driver.FindElement(locator);
        }

        public ReadOnlyCollection<IWebElement> FindElements(By locator)
        {
            return _driver.FindElements(locator);
        }
        #endregion

        #region Waiters
        public WebDriverWait Waiters() => new (_driver, TestSettings.WebDriverTimeOut);

        public void ImplicitWaiter(int waitInSeconds) => _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(waitInSeconds);
        #endregion

        public Actions Action => new(_driver);

        public object ExecuteScript(string script, params object[] args)
        {
            try
            {
                return ((IJavaScriptExecutor)_driver).ExecuteScript(script, args);
            }
            catch (WebDriverTimeoutException e)
            {
                Logger.Info($"Error: Timeout Exception thrown while running JS Script:{e.Message}-{e.StackTrace}");
            }
            return null;
        }
    }
}