using Core.Drivers;
using Core.Element;

using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

using SeleniumExtras.WaitHelpers;

namespace Test.Components
{
    public static class WebComponentExtension
    {
        public static void ClickSubElement(this CalendarComponent parentWebbObject, WebObject childWebObject)
        {
            WebDriverWait wait = new WebDriverWait(BrowserFactory.GetWebDriver(), TimeSpan.FromSeconds(10));

            // Wait for the parent element to be clickable
            IWebElement parent = wait.Until(ExpectedConditions.ElementToBeClickable(parentWebbObject.By));

            // Wait for the child element inside the parent
            IWebElement child = wait.Until(drv =>
            {
                IWebElement el = parent.FindElement(childWebObject.By);
                return (el != null && el.Displayed && el.Enabled) ? el : null;
            });

            child.Click();

        }
        public static IWebElement WaitSubElementToBeVisible(this CalendarComponent parentWebbObject, WebObject childWebObject)
        {
            WebDriverWait wait = new WebDriverWait(BrowserFactory.GetWebDriver(), TimeSpan.FromSeconds(10));

            // Wait for the parent element to be clickable
            IWebElement parent = wait.Until(ExpectedConditions.ElementToBeClickable(parentWebbObject.By));

            // Wait for the child element inside the parent
            IWebElement child = wait.Until(drv =>
            {
                IWebElement el = parent.FindElement(childWebObject.By);
                return (el != null && el.Displayed) ? el : null;
            });
            return child;
        }
        public static IWebElement WaitSubElementToBeClickable(this CalendarComponent parentWebbObject, WebObject childWebObject)
        {
            WebDriverWait wait = new WebDriverWait(BrowserFactory.GetWebDriver(), TimeSpan.FromSeconds(10));

            // Wait for the parent element to be clickable
            IWebElement parent = wait.Until(ExpectedConditions.ElementToBeClickable(parentWebbObject.By));

            // Wait for the child element inside the parent
            IWebElement child = wait.Until(drv =>
            {
                IWebElement el = parent.FindElement(childWebObject.By);
                return (el != null && el.Displayed && el.Enabled) ? el : null;
            });
            return child;
        }
    }
}