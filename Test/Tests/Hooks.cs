using Core.Reports;
using Core.ShareData;
using Core.Utils;

namespace Test.Tests
{
    [SetUpFixture]
    public class Hooks
    {
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            TestContext.Progress.WriteLine("================> Global OneTimeSetUp");
            ConfigurationUtils.ReadConfiguration("Configurations\\appsettings.json");
            DataStorage.InitData();
            var reportPaths = new string[]{
                Directory.GetCurrentDirectory()+"\\TestResults\\Latest-test.html",
                Directory.GetCurrentDirectory()+$"\\TestResults\\Test-{DateTime.Now.ToString("yyyyMMdd HHmmss")}.html"
            };
            ExtentReportHelper.InitualizeReport(reportPaths);
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            TestContext.Progress.WriteLine("================> Global OneTimeTearDown");
            ExtentReportHelper.Flush();
        }
    }
}