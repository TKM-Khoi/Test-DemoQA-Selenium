using Core.Element;

using OpenQA.Selenium;

using Service.Models.DTOs;

using Test.Components;

namespace Test.PageObjects
{
    public class ProfilePage : BasePage
    {
        private DeleteBookGridComponent _bookSearchTbl = new DeleteBookGridComponent();
        private WebObject _searchBoxTxt = new WebObject(By.Id("searchBox"), "Search Box");
        // private WebObject _      
        public void Search(string search)
        {
            _searchBoxTxt.EnterText(search);
        }
        public void DeleteBook(string name)
        {
            _bookSearchTbl.DeleteBook(name);
        }
        public IEnumerable<SearchedBookDto> GetBooks()
        {
            return _bookSearchTbl.GetAllResults();
        }
        public string GetNoDataWarningMsg()
        {
            return _bookSearchTbl.GetNoDataWarningMsg();
        }
    }
}