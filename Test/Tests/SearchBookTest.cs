using FluentAssertions;

using Service.Models.DTOs;

using Test.DataProvider;
using Test.PageObjects;

namespace Test.Tests
{
    public class SearchBookTest : BaseTest
    {
        private BookStorePage _bookStorePage = new BookStorePage();
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [TestCaseSource(typeof(SearchBookDataProvider), nameof(SearchBookDataProvider.ValidSearch))]
        public void ValidSearch(string search)
        {
            _bookStorePage.NavigateToPage();
            _bookStorePage.Search(search);
            IEnumerable<SearchedBookDto> books = _bookStorePage.GetSearchedBooks();
            // Assert.That(books.Count(), Is.EqualTo(2));
            books.Should().AllSatisfy(book =>
            {
                (book.Image.Contains(search, StringComparison.InvariantCultureIgnoreCase) ||
                book.Title.Contains(search, StringComparison.InvariantCultureIgnoreCase) ||
                book.Author.Contains(search, StringComparison.InvariantCultureIgnoreCase) ||
                book.Publisher.Contains(search, StringComparison.InvariantCultureIgnoreCase)
                ).Should().BeTrue();
            });
        }
        // [Ignore("")]
        [Test]
        [TestCaseSource(typeof(SearchBookDataProvider), nameof(SearchBookDataProvider.EmptySearch))]
        public void EmptySearch(string search)
        {
            _bookStorePage.NavigateToPage();
            _bookStorePage.Search(search);
            IEnumerable<SearchedBookDto> books = _bookStorePage.GetSearchedBooks();
            // Assert.That(books.Count(), Is.EqualTo(2));
            books.Should().AllSatisfy(book =>
            {
                ( book.Title.Contains(search, StringComparison.InvariantCultureIgnoreCase) 
                || book.Image.Contains(search, StringComparison.InvariantCultureIgnoreCase)
                || book.Author.Contains(search, StringComparison.InvariantCultureIgnoreCase) 
                || book.Publisher.Contains(search, StringComparison.InvariantCultureIgnoreCase)
                ).Should().BeTrue();
            });
        }
    }
}