using Core.Element;
using Core.Utils;

using OpenQA.Selenium;

using Test.Components;
using Test.Const;
using Test.DataModels;

namespace Test.PageObjects
{
    public class RegisterPage
    {
        #region locator
        private WebObject _firstnameTxt = new WebObject(By.Id("firstName"), "First Name Text Box");
        private WebObject _lastnameTxt = new WebObject(By.Id("lastName"), "Last Name Text Box");
        private WebObject _emailTxt = new WebObject(By.Id("userEmail"), "Email Text Box");
        private WebObject GenderRdo(string gender) => new WebObject(By.XPath($"//label[text()='{gender}']"), "Gender Radio Option");
        private WebObject _mobileTextbox = new WebObject(By.Id("userNumber"), "Gender Option");
        private CalendarComponent _birthDayDtp = new CalendarComponent(By.Id("dateOfBirthInput"), "Birthday Date Picker");
        private WebObject _subjectsTxt = new WebObject(By.Id("subjectsInput"), "Subjects Search Box");
        private WebObject _subjectsSearchResult = new WebObject(By.ClassName("subjects-auto-complete__menu"), "Subjects Search Result");
        private WebObject SubjectsOpt(string subject) => new WebObject(By.XPath($"//div[contains(@class, 'subjects-auto-complete__option') and text()='{subject}']"), "Subjects Search Box");
        private WebObject HobbyChk(string hobby) => new WebObject(By.XPath($"//label[text()='{hobby}']"), "Hobby Check Box Option");
        private WebObject _pictureFile = new WebObject(By.Id("uploadPicture"), "Picture Picker");
        private WebObject _addrTxa = new WebObject(By.Id("currentAddress"), "Current Address Text Area");
        private WebObject _stateDdl = new WebObject(By.Id("state"), "State Dropdown List");
        private WebObject StateOpt(string state) => new WebObject(By.XPath($"//div[@id='stateCity-wrapper']//div[text()='{state}'] "), "State Option");
        private WebObject _cityDdl = new WebObject(By.Id("city"), "City Dropdown List");
        private WebObject CityOpt(string city) => new WebObject(By.XPath($"//div[@id='stateCity-wrapper']//div[text()='{city}'] "), "City Option");
        private WebObject _submitBtn = new WebObject(By.Id("submit"), "Submit Button");
        private RegisterSuccessPopupComponent _resultPopupFra = new RegisterSuccessPopupComponent(By.ClassName("modal-body"), "Result Popup");
        #endregion

        #region action
        public void NavigateToPage()
        {
            DriverUtils.GoToUrl(ConfigurationUtils.GetConfigurationByKey("TestUrl") + PageUrlConst.REGISTER_PAGE);
        }
        public void EnterFirstName(string firstName)
        {
            _firstnameTxt.EnterText(firstName);
        }
        public void EnterLastName(string lastName)
        {
            _lastnameTxt.EnterText(lastName);
        }
        public void EnterEmail(string email)
        {
            _emailTxt.EnterText(email);
        }
        public void SelectGender(string gender)
        {
            GenderRdo(gender).ClickOnElement();
        }
        public void EnterPhone(string phone)
        {
            _mobileTextbox.EnterText(phone);
        }
        public void SelectDob(DateTime dob)
        {
            _birthDayDtp.SelectDate(dob);
        }
        public void EnterSubject(List<string> subjects)
        {
            IWebElement subjectEl = _subjectsTxt.WaitForElementToBeClickable();
            foreach (string subject in subjects)
            {
                _subjectsTxt.EnterText(subject, false);
                _subjectsSearchResult.WaitForElementToBeVisible();
                // _subjectsTxt.EnterText("\t", false);//Only work for chrome, not firefox
                SubjectsOpt(subject).ClickOnElement();
            }
        }
        public void SelectHobbies(List<string> hobbies)
        {
            foreach (string hobby in hobbies)
            {
                HobbyChk(hobby).ClickOnElement();
            }
        }
        public void SelectPicture(string picturePath)
        {
            _pictureFile.EnterText(picturePath.GetAbsolutePath());
        }
        public void EnterAddress(string address)
        {
            _addrTxa.EnterText(address);
        }
        public void SelectState(string state)
        {
            _stateDdl.ClickOnElement();
            StateOpt(state).ClickOnElement();
        }
        public void SelectCity(string city)
        {
            _cityDdl.ClickOnElement();
            CityOpt(city).ClickOnElement();
        }
        public void ClickSubmit()
        {
            _submitBtn.ClickOnElement();
        }
        public void RegisterStudent(RegisterData dto)
        {
            EnterFirstName(dto.FirstName);
            EnterLastName(dto.LastName);
            if (dto.Email != null)
            {
                EnterEmail(dto.Email);
            }
            SelectGender(dto.Gender);
            EnterPhone(dto.Phone);
            SelectDob(dto.DateOfBirth);
            if (dto.Subjects != null)
            {
                EnterSubject(dto.Subjects.ToList());
            }
            if (dto.Picture != null)
            {
                SelectPicture(dto.Picture);
            }
            if (dto.Hobbies != null)
            {
                SelectHobbies(dto.Hobbies.ToList());
            }
            if (dto.Address != null)
            {
                EnterAddress(dto.Address);
            }
            if (dto.State != null)
            {
                SelectState(dto.State);
            }
            if (dto.City != null)
            {
                SelectCity(dto.City);
            }
            ClickSubmit();
        }
        public dynamic GetRegiseteredStudent()
        {
            return _resultPopupFra.GetRegisterResult();
        }

        public string GetRegisterSuccessfullyMessage()
        {
            return _resultPopupFra.GetHeaderText();
        }
        #endregion

    }
}