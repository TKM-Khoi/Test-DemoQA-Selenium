using Core.Element;
using Core.Reports;
using Core.Utils;

using OpenQA.Selenium;

using Test.Const;

namespace Test.PageObjects
{
    public class LoginPage : BasePage
    {
        #region elements
        private readonly WebObject _usernameTxt = new WebObject(By.Id("userName"), "Username Text Box");
        private readonly WebObject _passwordTxt = new WebObject(By.Id("password"), "Password Text Box");
        private readonly WebObject _loginBtn = new WebObject(By.Id("login"), "Login button");
        #endregion
        public void NavigateToPage()
        {
            DriverUtils.GoToUrl(ConfigurationUtils.GetConfigurationByKey("TestUrl")+ PageUrlConst.LOGIN_PAGE);
        }
        public void EnterUsername(string username)
        {
            ExtentReportHelper.LogTestStep($"{_usernameTxt.Name} select {username}");
            _usernameTxt.EnterText(username);
        }
        public void EnterPassword(string password){
            ExtentReportHelper.LogTestStep($"{_passwordTxt.Name} select {password}");
            _passwordTxt.EnterText(password);
        }
        public void ClickLogin(){
            ExtentReportHelper.LogTestStep($"Click {_loginBtn.Name}");
            _loginBtn.ClickOnElement();
        }
        public void Login(string username, string password){
            EnterUsername(username);
            EnterPassword(password);
            ClickLogin();
        }
      
    }
}