namespace TAF.Core.Utilities
{
    public static class Waiters
    {
        public static void WaitForPageLoad() => Browser.Browser.NewBrowser.Waiters().Until(condition => Browser.Browser.NewBrowser.ExecuteScript("return document.readyState").Equals("complete"));
        
        public static void WaitForCondition(Func<bool> condition) => Browser.Browser.NewBrowser.Waiters().Until(x => condition.Invoke());
    }
}