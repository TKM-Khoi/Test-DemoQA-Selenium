using System.Dynamic;

using Core.Element;

using OpenQA.Selenium;

namespace Test.Components
{
    public class RegisterSuccessPopupComponent : WebObject
    {
        public RegisterSuccessPopupComponent(By by, string name = "") : base(by, name)
        {
        }
        private WebObject _confirmTbl = new WebObject(By.CssSelector(".modal-body table tbody"), "Register Success Confirm Table");
        private WebObject _thankYouHdr = new WebObject(By.Id("example-modal-sizes-title-lg"), "Thank you header");
        public dynamic GetRegisterResult()
        {
            dynamic result = new ExpandoObject();
            var dict = (IDictionary<string, object>)result;
            var rows = _confirmTbl.WaitForElementToBeVisible().FindElements(By.TagName("tr")).ToList();
            foreach (var row in rows)
            {
                var cells = row.FindElements(By.TagName("td"));
                string key = cells[0].Text;
                string value = cells[1].Text;
                dict[key] = value;
            }
            return result;
        }
           public string GetHeaderText()
        {
            return _thankYouHdr.GetTextFromElement();
        }
    }
}