namespace TAF.Core.Utilities
{
    public static class Waiters
    {
        private static Browser.Browser _browser;
        public static void WaitForPageLoad() => _browser.Waiters().Until(condition => _browser.ExecuteScript("return document.readyState").Equals("complete"));
        
        public static void WaitForCondition(Func<bool> condition) => _browser.Waiters().Until(x => condition.Invoke());
    }
}