using System.Dynamic;
using System.Text.Json;

using Test.DataModels;
using Test.DataProvider;
using Test.PageObjects;

namespace Test.Tests
{
    public class RegisterTest : BaseTest
    {
        private RegisterPage _registerPage = new RegisterPage();
        [Test]
        [Category("Register")]
        [TestCaseSource(typeof(RegisterDataProvider), nameof(RegisterDataProvider.ValidAllFieldsRegister))]
        public void RegisterWithAllFieldsSuccessfullyTest(RegisterData dto)
        {
            _registerPage.NavigateToPage();
            _registerPage.RegisterStudent(dto);

            var actualResult = JsonSerializer.Serialize(_registerPage.GetRegiseteredStudent());
            var expectedResult = JsonSerializer.Serialize(CreateCustomObject(dto));

            Assert.That(actualResult, Is.EqualTo(expectedResult));  
        }
        [Test]
        [Category("Register")]
        [TestCaseSource(typeof(RegisterDataProvider), nameof(RegisterDataProvider.ValidRequiredFieldsRegister))]
        public void RegisterWithRequiredFieldsSuccessfullyTest(RegisterData dto)
        {
            _registerPage.NavigateToPage();
            _registerPage.RegisterStudent(dto);

            var actualResult = JsonSerializer.Serialize(_registerPage.GetRegiseteredStudent());
            var expectedResult = JsonSerializer.Serialize(CreateCustomObject(dto));

            Assert.That(actualResult, Is.EqualTo(expectedResult));  
        }
        private dynamic CreateCustomObject(RegisterData dto)
        {
            dynamic obj = new ExpandoObject();
            var dict = (IDictionary<string, object>)obj;

            dict["Student Name"] = dto.FirstName + " " + dto.LastName;
            dict["Student Email"] = dto.Email??"";
            obj.Gender = dto.Gender.ToString();
            obj.Mobile = dto.Phone;
            dict["Date of Birth"] = dto.DateOfBirth.ToString("dd MMM,yyyy");
            obj.Subjects = dto.Subjects==null?"": String.Join(", ", dto.Subjects);
            obj.Hobbies = dto.Hobbies == null? "": String.Join(", ", dto.Hobbies);
            obj.Picture = dto.Picture==null?"": dto.Picture.Substring(dto.Picture.LastIndexOf("\\") + 1);
            obj.Address = dto.Address??"";
            dict["State and City"] = dto.State==null?"":
                dto.State + " " + dto.City??"";
            return obj;
        }
    }
}