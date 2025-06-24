using Core.Utils;

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;

namespace Core.Drivers
{
    public static class BrowserFactory
    {
        private static AsyncLocal<IWebDriver> AsyncLocalWebDriver = new AsyncLocal<IWebDriver>();
        private static AsyncLocal<WebDriverWait> AsyncLocalWait = new AsyncLocal<WebDriverWait>();
        public static void InitializeDriver(string browserName, string[]? args = null, IDictionary<string, string>? prefs = null)
        {
            TimeSpan timeout = TimeSpan.FromSeconds(Int32.Parse(ConfigurationUtils.GetConfigurationByKey("WebDriver.Wait.Time")));
            switch (browserName.ToLower())
            {
                case "chrome":
                    {
                        new DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);
                        ChromeOptions chromeOptions = new ChromeOptions();
                        if (args != null && args.Length != 0)
                        {
                            foreach (var arg in args)
                            {
                                chromeOptions.AddArguments(arg);
                            }
                        }
                        ChromeDriver chromeDriver = new ChromeDriver(chromeOptions);
                        AsyncLocalWebDriver.Value = chromeDriver;
                        AsyncLocalWait.Value = new WebDriverWait(chromeDriver, timeout);
                        break;
                    }
                case "firefox":
                    {
                        new DriverManager().SetUpDriver(new FirefoxConfig(), VersionResolveStrategy.Latest);
                        var firefoxOptions = new FirefoxOptions();
                        if (args != null && args.Length > 0)
                        {
                            firefoxOptions.AddArguments(args);
                        }
                        if (prefs != null)
                        {
                            foreach (var kv in prefs)
                            {
                                firefoxOptions.SetPreference(kv.Key, kv.Value);
                            }
                        }
                        FirefoxDriver firefoxDriver = new(firefoxOptions);
                        AsyncLocalWebDriver.Value = firefoxDriver;
                        AsyncLocalWait.Value = new WebDriverWait(firefoxDriver, timeout);
                        break;
                    }
                default: throw new ArgumentException("InitualizeDriver: Not a valid driver");
            }
        }
        public static void CleanUpWebDriver()
        {
            if (GetWebDriver() != null)
            {
                GetWebDriver().Quit();
                GetWebDriver().Dispose();
            }
        }
        public static IWebDriver GetWebDriver()
        {
            return AsyncLocalWebDriver.Value;
        }
        public static WebDriverWait GetDriverWait()
        {
            return AsyncLocalWait.Value;
        }
    }
}