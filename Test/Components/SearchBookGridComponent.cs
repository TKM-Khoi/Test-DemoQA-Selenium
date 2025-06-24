using Core.Drivers;
using Core.Element;

using OpenQA.Selenium;

using Service.Models.DTOs;

namespace Test.Components
{
    public class SearchBookGridComponent
    {
        #region state
        private bool _isMaxPageSize = false;
        private int _imgCollumnNum = -1;
        private int _titleCollumnNum = -1;
        private int _authorCollumnNum = -1;
        private int _publisherCollumnNum = -1;
        // Is the headers index found
        private bool _isHeaderCollumnIndexReady = false;
        #endregion
        #region locator
        private WebObject _currentPageNumTxt = new WebObject(By.CssSelector(".-pageInfo input"), "Current Page Input");
        private WebObject _totalPageNumLbl = new WebObject(By.ClassName("-totalPages"), "Total Pages Lable");
        private WebObject _tableHeaderLbl = new WebObject(By.ClassName("rt-resizable-header-content"));
        private WebObject _resultRows = new WebObject(By.CssSelector(".rt-tbody .rt-tr:has(img)"));
        private WebObject ResultCell(int row, int collumn) => new WebObject(By.CssSelector($".rt-tbody .rt-tr-group:nth-child({row}) .rt-td:nth-child({collumn})"));
        private WebObject ResultImageCell(int row) => new WebObject(By.CssSelector($".rt-tbody .rt-tr-group:nth-child({row}) img"));
        private WebObject _pageSizeDdl = new WebObject(By.CssSelector(".-pageSizeOptions select"));
        private WebObject _minPageSizeOpt = new WebObject(By.CssSelector(".-pageSizeOptions select option:first-child"));
        private WebObject _maxPageSizeOpt = new WebObject(By.CssSelector(".-pageSizeOptions select option:last-child"));
        private WebObject _nextBtn = new WebObject(By.XPath("//button[text()='Next']"));
        private WebObject _noResultWarning = new WebObject(By.ClassName("rt-noData"));
        #endregion
      
        public int GetCurrentPage()
        {
            string? currentPageString = _currentPageNumTxt.WaitForElementToBeClickable().GetAttribute("value");
            return String.IsNullOrWhiteSpace(currentPageString) ? -1 : Int32.Parse(currentPageString);
        }
        public void ChangeToMaxPageSize()
        {
            _pageSizeDdl.ClickOnElement();
            _maxPageSizeOpt.ClickOnElement();
            _pageSizeDdl.ClickOnElement();
        }
        public void ChangeToMinPageSize()
        {
            _pageSizeDdl.ClickOnElement();
            _minPageSizeOpt.ClickOnElement();
            _pageSizeDdl.ClickOnElement();
        }
        public int GetTotalPage()
        {
            string? totalPageString = _totalPageNumLbl.GetTextFromElement();
            return String.IsNullOrWhiteSpace(totalPageString) ? -1 : Int32.Parse(totalPageString);
        }
        public IEnumerable<SearchedBookDto> GetAllResults()
        {
            SetCollumnIndexes();
            // ChangeToMinPageSize();

            if (GetCurrentPage() < GetTotalPage())
            {
                ChangeToMaxPageSize();
            }
            IEnumerable<SearchedBookDto> books = new List<SearchedBookDto>();
            do
            {
                if (BrowserFactory.GetWebDriver().FindElements(_resultRows.By).Count == 0)
                {
                    return books;                    
                }
                List<IWebElement> bookRows = _resultRows.WaitForAllElementsToBeVisible();
                for (int currentRow = 1; currentRow <= bookRows.Count; currentRow++)
                {
                    SearchedBookDto bookInRow = new SearchedBookDto();
                    if (_imgCollumnNum > 0)
                    {
                        bookInRow.Image = ResultImageCell(currentRow).WaitForElementToBeVisible().GetAttribute("src");
                    }
                    if (_titleCollumnNum > 0)
                    {
                        bookInRow.Title = ResultCell(currentRow, _titleCollumnNum).GetTextFromElement();
                    }
                    if (_authorCollumnNum > 0)
                    {
                        bookInRow.Author = ResultCell(currentRow, _authorCollumnNum).GetTextFromElement();
                    }
                    if (_publisherCollumnNum > 0)
                    {
                        bookInRow.Publisher = ResultCell(currentRow, _publisherCollumnNum).GetTextFromElement();
                    }
                    books = books.Append(bookInRow);
                }
                
                int current = GetCurrentPage();
                int total = GetTotalPage();
                if (current >= total)
                {
                    break;
                }
                ClickNextPage();

            } while (_nextBtn.WaitForElementToBeVisible().Enabled);
            return books;
        }
        public void ClickNextPage() {
            if (_nextBtn.WaitForElementToBeVisible().Enabled)
            {
                _nextBtn.ClickOnElement();
            }
        }
        public string GetNoDataWarningMsg()
        {
            return _noResultWarning.GetTextFromElement();
        }
        private void SetCollumnIndexes()
        {
            if (_isHeaderCollumnIndexReady)
            {
                return;
            }
            string imgHeader = "image";
            string titleHeader = "title";
            string authorHeader = "author";
            string publisherHeader = "publisher";
            var headers = _tableHeaderLbl.GetTextFromAllElements();
            for (int i = 0; i < headers.Count; i++)
            {
                if (headers[i].ToLower().Contains(imgHeader)
                    || imgHeader.Contains(headers[i].ToLower()))
                {
                    _imgCollumnNum = i + 1;
                }
                else if (headers[i].ToLower().Contains(titleHeader)
                    || titleHeader.Contains(headers[i].ToLower()))
                {
                    _titleCollumnNum = i + 1;
                }
                else if (headers[i].ToLower().Contains(authorHeader)
                    || authorHeader.Contains(headers[i].ToLower()))
                {
                    _authorCollumnNum = i + 1;
                }
                else if (headers[i].ToLower().Contains(publisherHeader)
                    || publisherHeader.Contains(headers[i].ToLower()))
                {
                    _publisherCollumnNum = i + 1;
                }
            }
            _isHeaderCollumnIndexReady = true;
        }
    }
}