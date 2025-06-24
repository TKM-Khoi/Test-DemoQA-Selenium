using Core.Drivers;
using Core.Reports;
using Core.ShareData;
using Core.Utils;

using NUnit.Framework.Interfaces;

namespace Test.Tests
{
    [Parallelizable(ParallelScope.Fixtures)]
    public class BaseTest
    {
        [OneTimeSetUp]
        public void CreateTestForExtendReport()
        {
            ExtentReportHelper.CreateTest(TestContext.CurrentContext.Test.ClassName);
        }
        [SetUp]
        public void SetUp()
        // public async Task SetUp()
        {
            Console.WriteLine("BaseTest: SetUp");
            string browserName = ConfigurationUtils.GetConfigurationByKey("Browser:Name");
            var args = ConfigurationUtils.GetBrowserArgs($"Browser:{browserName}:BrowserArgs");
            var prefs = ConfigurationUtils.GetBrowserPrefs(browserName);
            BrowserFactory.InitializeDriver(browserName, args.ToArray(), prefs);
            ExtentReportHelper.CreateNode(TestContext.CurrentContext.Test.MethodName);
            DriverUtils.MaximizeWindow();
        }
        [TearDown]
        public void TearDown()
        {
            Console.WriteLine("BaseTest: TearDown");
            TestStatus status = TestContext.CurrentContext.Result.Outcome.Status;
            var stackTrace = string.IsNullOrEmpty(TestContext.CurrentContext.Result.StackTrace) ? "" : TestContext.CurrentContext.Result.StackTrace;
            ExtentReportHelper.CreateTestResult(Enum.GetName(typeof(TestStatus), status), stackTrace, TestContext.CurrentContext.Test.ClassName, TestContext.CurrentContext.Test.Name);
            DataStorage.ClearData();
            BrowserFactory.CleanUpWebDriver();
        }
    }
}