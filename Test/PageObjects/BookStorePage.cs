using Core.Element;
using Core.Utils;

using OpenQA.Selenium;

using Service.Models.DTOs;

using Test.Components;
using Test.Const;

namespace Test.PageObjects
{
    public class BookStorePage : BasePage
    {
        #region locators
        // private SearchBookGrid _bookSearchTbl = new BookSearchGridObject(By.ClassName("books-wrapper"), "Book Search Table");
        private SearchBookGridComponent _bookSearchTbl = new SearchBookGridComponent();
        private WebObject _searchBoxTxt = new WebObject(By.Id("searchBox"), "Search Box");

        #endregion

        #region Actions
        public void NavigateToPage()
        {
            DriverUtils.GoToUrl(ConfigurationUtils.GetConfigurationByKey("TestUrl") + PageUrlConst.BOOKSTORE_PAGE);
        }
        public void Search(string search)
        {
            _searchBoxTxt.EnterText(search);
        }
        public IEnumerable<SearchedBookDto> GetSearchedBooks()
        {
            return _bookSearchTbl.GetAllResults();
        }
        #endregion
    }
}