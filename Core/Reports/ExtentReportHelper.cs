using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;

using Core.Drivers;
using Core.Utils;

namespace Core.Reports
{
    public class ExtentReportHelper
    {
        static ExtentReports ExtentManager;
        // [ThreadStatic]
        // public static ExtentTest ExtentTest;
        // // [ThreadStatic]
        // public static ExtentTest Node;
        private static AsyncLocal<ExtentTest> ExtentTest = new AsyncLocal<ExtentTest>();

        private static AsyncLocal<ExtentTest> Node = new AsyncLocal<ExtentTest>();
        public static void InitualizeReport(string[] reportPaths)
        {
            ExtentManager = new ExtentReports();
            foreach (string reportPath in reportPaths)
            {
                ExtentSparkReporter htmlReporter = new ExtentSparkReporter(reportPath);
                ExtentManager.AttachReporter(htmlReporter);
            }
            ExtentManager.AddSystemInfo("Enviroment", "Staging");
            ExtentManager.AddSystemInfo("Browser", ConfigurationUtils.GetConfigurationByKey("Browser:Name"));
            Console.WriteLine("Initualize report");
        }
        public static void CreateTest(string name){
            ExtentTest.Value = ExtentManager.CreateTest(name);
            Console.WriteLine("create test");
        }  
        public static void CreateNode(string name){
            Node.Value = ExtentTest.Value.CreateNode(name);
            Console.WriteLine("create node");
        }
        public static void LogTestStep(string step){
            Node.Value.Info(step);
        }
        public static void CreateTestResult(string status, string stackTrace, string className, string testName){
            Status logStatus;
            switch(status){
                case "Failed":{
                    logStatus = Status.Fail;
                    if(BrowserFactory.GetWebDriver() != null){
                        string mediaEntity = DriverUtils.CaptureScreenshot(BrowserFactory.GetWebDriver(), className, testName);
                        Node.Value.Fail($"#Test Name: {testName}, #Status: {logStatus+stackTrace}, #Img: {mediaEntity}").AddScreenCaptureFromPath(mediaEntity);
                    }else{
                        Node.Value.Fail($"#Test Name: {testName}, #Status: {logStatus+stackTrace}");
                    }
                    break;
                }
                case "Passed":{
                    logStatus = Status.Pass;
                    Node.Value.Pass($"====> Test Name: {testName}, Status: {logStatus}");
                    break;
                }
                case "Inconclusive":{
                    logStatus = Status.Warning;
                    Node.Value.Pass($"====> Test Name: {testName}, Status: {logStatus}");
                    break;
                }
                case "Skipped":{
                    logStatus = Status.Skip;
                    Node.Value.Pass($"====> Test Name: {testName}, Status: {logStatus}");
                    break;
                }
                default:{
                    logStatus = Status.Pass;
                    Node.Value.Pass($"====> Test Name: {testName}, Status: {logStatus}");
                    break;
                }
            }
        }
        public static void Flush(){
            Console.WriteLine("Before flush");
            ExtentManager.Flush();
            Console.WriteLine("After flush");
        }   
    }
}