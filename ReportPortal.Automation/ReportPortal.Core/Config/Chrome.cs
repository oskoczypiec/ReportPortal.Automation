using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace ReportPortal.Core.Config
{
    public class Chrome
    {
        public IWebDriver ConfigureChromeDriver()
        {
            ChromeOptions options = new ();
            options.AddExcludedArgument("--enable-automation");
            options.AddArgument("--disable-popup-blocking");
            options.AddArgument("--no-sandbox");
           // options.AddArgument("--headless");
            //options.AddArgument("--incognito");

            return new ChromeDriver(AppDomain.CurrentDomain.BaseDirectory, options);
        }
    }
}