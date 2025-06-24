using Core.Element;

using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Test.Components
{
    public class CalendarComponent : WebObject
    {
        #region sub elements 
        private WebObject _monthDdl = new WebObject(By.ClassName("react-datepicker__month-select"), "Month Picker Dropdown List");
        private WebObject _yearDdl = new WebObject(By.ClassName("react-datepicker__year-select"), "Year Picker Dropdown List");
        private WebObject DateBtn(DateTime date)
            => new WebObject(By.XPath($"//div[@aria-label='Choose {FormatDateWithSuffix(date)}']"), $"Day Button");
        #endregion

        public CalendarComponent(By by, string name = "") : base(by, name)
        { }
       
        public void SelectDate(DateTime date)
        {
            this.ClickOnElement();
           SelectYear(date.Year);
           SelectMonth(date.Month);
            this.ClickSubElement(DateBtn(date));
        }
        private void SelectMonth(int month)
        {
            IWebElement monthEl =_monthDdl.WaitForElementToBeClickable();
            new SelectElement(monthEl).SelectByValue((month-1)+"");
        }
        private void SelectYear(int year)
        {
            IWebElement yearEl =_yearDdl.WaitForElementToBeClickable();
            new SelectElement(yearEl).SelectByValue(year+"");
        }
        private string FormatDateWithSuffix(DateTime date)
        {
            int day = date.Day;
            string suffix = GetDaySuffix(day);
            return $"{date:dddd}, {date:MMMM} {day}{suffix}, {date:yyyy}";
        }

        private string GetDaySuffix(int day)
        {
            if (day >= 11 && day <= 13)
                return "th";
            switch (day % 10)
            {
                case 1: return "st";
                case 2: return "nd";
                case 3: return "rd";
                default: return "th";
            }
        }
    }
}