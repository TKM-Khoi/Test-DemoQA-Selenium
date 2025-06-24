using Core.Drivers;
using Core.Reports;

using OpenQA.Selenium;
using OpenQA.Selenium.DevTools;
using OpenQA.Selenium.DevTools.V136.Network;

namespace Core.Utils
{
    public class DriverUtils
    {
        public static void GoToUrl(string url)
        {
            // IWebDriver webDriver = BrowserFactory.GetWebDriver();
            // webDriver.Navigate().GoToUrl(url);
            BrowserFactory.GetWebDriver().Navigate().GoToUrl(url);
             
        }
        public static void MaximizeWindow()
        {
            BrowserFactory.GetWebDriver().Manage().Window.Maximize();
        }
        public static string CaptureScreenshot(IWebDriver driver, string className, string testName)
        {
            ITakesScreenshot ts = (ITakesScreenshot)driver;
            Screenshot screenshot = ts.GetScreenshot();
            var screenshotDirectory = Path.Combine(Directory.GetCurrentDirectory(), ConfigurationUtils.GetConfigurationByKey("Screenshot.Folder"));
            testName = testName.Replace("\"", "");
            var fileName = $"Screenshot_{testName}_{DateTime.Now.ToString("yyyyMMdd_HHmmssff")}";
            Directory.CreateDirectory(screenshotDirectory);
            var fileFullName = $"{screenshotDirectory}\\{fileName}.png";
            screenshot.SaveAsFile(fileFullName);
            return fileFullName;
        }
        // public static void SendKeys(IWebElement el, string keys)
        // {
        //     Actions actions1 = new Actions(BrowserFactory.GetWebDriver());
        //     actions1.Click(el).Perform();
        //     // Actions actions2 = new Actions(BrowserFactory.GetWebDriver());
        //     actions1.SendKeys(keys).Perform();
        //     actions1.SendKeys("\t").Perform();
        // }
        public static void CloseAndCleanUp()
        {
            var driver = BrowserFactory.GetWebDriver();
            driver.Quit();
            driver.Dispose();
        }
        public static async Task SetUpApiListener()
        {
            var devTools = BrowserFactory.GetWebDriver() as IDevTools;
            var session = devTools.GetDevToolsSession();
            OpenQA.Selenium.DevTools.V136.DevToolsSessionDomains domains = session.GetVersionSpecificDomains<OpenQA.Selenium.DevTools.V136.DevToolsSessionDomains>();

            await domains.Network.Enable(new EnableCommandSettings());
            await domains.Network.SetCacheDisabled(new SetCacheDisabledCommandSettings { CacheDisabled = true });

            // Hook: Request sent
            domains.Network.RequestWillBeSent += (sender, e) =>
            {
                ExtentReportHelper.LogTestStep($"Send request: {e.Request.Method} {e.Request.Url}");
            };

            // Hook: Response received
            domains.Network.ResponseReceived += async (sender, response) =>
            {
                string log = ($"Received response: {response.Response.Status} {response.Response.Url}<br/>");

                try
                {
                    var bodyResult = await domains.Network.GetResponseBody(new GetResponseBodyCommandSettings
                    {
                        RequestId = response.RequestId
                    });

                    log += ($"Body: {bodyResult.Body.Substring(0, Math.Min(200, bodyResult.Body.Length))}...");
                }
                catch (Exception ex)
                {
                    log += ($"Could not read body: {ex.Message}");
                }
                finally
                {
                    ExtentReportHelper.LogTestStep(log);
                }
            };
        }

    }
}