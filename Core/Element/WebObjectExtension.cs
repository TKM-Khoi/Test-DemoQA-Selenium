using Core.Drivers;

using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

using SeleniumExtras.WaitHelpers;

namespace Core.Element
{
    public static class WebObjectExtension
    {
        public static WebDriverWait Wait() => BrowserFactory.GetDriverWait();
        public static IWebDriver WebDriver() => BrowserFactory.GetWebDriver();

        public static IWebElement WaitForElementToBeVisible(this WebObject webObject)
        {
            return BrowserFactory.GetDriverWait().Until(ExpectedConditions.ElementIsVisible(webObject.By));
        }
        public static List<IWebElement> WaitForAllElementsToBeVisible(this WebObject webObjects)
        {
            return Wait().Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(webObjects.By)).ToList();
        }
        public static IWebElement WaitForElementToBeClickable(this WebObject webObject)
        {
            return Wait().Until(ExpectedConditions.ElementToBeClickable(webObject.By));
        }
        public static string GetTextFromElement(this WebObject webObject)
        {
            IWebElement element = IsElementDisplayed(webObject) ? WaitForElementToBeVisible(webObject) : null;
            if (element != null)
            {
                return element.Text;
            }
            return string.Empty;
        }
        public static List<string> GetTextFromAllElements(this WebObject webObject)
        {
            List<string> textList = new List<string>();
            List<IWebElement> elements = WaitForAllElementsToBeVisible(webObject).ToList();
            foreach (var element in elements)
            {
                textList.Add(element.Text);
            }
            return textList;
        }
        public static bool IsElementDisplayed(this WebObject webObject)
        {
            return Wait().Until(ExpectedConditions.ElementIsVisible(webObject.By)).Displayed;
        }
        public static void ClickOnElement(this WebObject webObject)
        {
            IWebElement element = WaitForElementToBeClickable(webObject);
            ((IJavaScriptExecutor)WebDriver()).ExecuteScript("arguments[0].scrollIntoView(true);", element);
            element.Click();
        }
        public static void EnterText(this WebObject webObject, string text, bool clearTest = true)
        {
            IWebElement element = WaitForElementToBeClickable(webObject);
            ((IJavaScriptExecutor)WebDriver()).ExecuteScript("arguments[0].scrollIntoView(true);", element);
            if (clearTest)
            {
                element.Clear();
            }
            element.SendKeys(text);
        }
    }
}