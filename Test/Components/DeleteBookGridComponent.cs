using Core.Drivers;
using Core.Element;

using OpenQA.Selenium;

using SeleniumExtras.WaitHelpers;

namespace Test.Components
{
    public class DeleteBookGridComponent : SearchBookGridComponent
    {
        private WebObject DeleteButton(string name) => new WebObject(By.XPath($"//a[text()='{name}']/ancestor::div[@role='row']//span[@title='Delete']"));
        private WebObject _confirmDeleteBtn = new WebObject(By.Id("closeSmallModal-ok"));
        public void DeleteBook(string name)
        {
            DeleteButton(name).ClickOnElement();
            _confirmDeleteBtn.ClickOnElement();
            // IAlert alert = BrowserFactory.GetWebDriver().SwitchTo().Alert();
            IAlert alert = BrowserFactory.GetDriverWait().Until(ExpectedConditions.AlertIsPresent());
            alert.Accept();
        }
    }
}