using System.Net;

using Core.Client;
using Core.ShareData;
using Core.Utils;

using FluentAssertions;

using Service.Models.DTOs;
using Service.Models.Resquests;
using Service.Services;

using Test.DataModels;
using Test.DataProvider;
using Test.PageObjects;

namespace Test.Tests
{
    public class DeleteBookTest : BaseTest
    {
        private AccountService _accountService;
        private BookService _bookService;
        private LoginPage _loginPage;
        private ProfilePage _profilePage;
        private ApiClient apiClient;
        [SetUp]
        public void SetUp()
        {
            apiClient = new ApiClient(ConfigurationUtils.GetConfigurationByKey("TestUrl"));
            _bookService = new BookService(apiClient);
            _accountService = new AccountService(apiClient);
            _loginPage = new LoginPage();
            _profilePage = new ProfilePage();
        }
        [Test]
        [TestCaseSource(typeof(DeleteBookDataProvider), nameof(DeleteBookDataProvider.ValidData))]
        public async Task DeleteBook(DeleteBookData dto)
        {
            AddBookRequest addBookdata = new AddBookRequest(dto.UserId, dto.Isbn);

            var addBookResponse = await _bookService.AddBookWithUnameAndPasswordAsync(addBookdata, dto.Username, dto.Password);
            if (addBookResponse.StatusCode == HttpStatusCode.Created)
            {
                _bookService.StoreBookCollectionToDeleteLater(dto.Isbn, dto.UserId, dto.Username, dto.Password);
            }

            _loginPage.NavigateToPage();
            _loginPage.Login(dto.Username, dto.Password);
            _profilePage.Search(dto.Title);
            _profilePage.DeleteBook(dto.Title);


            IEnumerable<SearchedBookDto> ownedBooks = _profilePage.GetBooks();
            ownedBooks.Should().AllSatisfy(book =>
            {
                book.Image.ToLower().Should().NotContainAll(dto.Title);
                book.Title.Should().NotContainAll(dto.Title);
                book.Author.ToLower().Should().NotContainAll(dto.Title);
                book.Publisher.ToLower().Should().NotContainAll(dto.Title);
            });

            _profilePage.Search(dto.Title);
            IEnumerable<SearchedBookDto> searchedOwnedBooks = _profilePage.GetBooks();
            var noBookFoundMsg = _profilePage.GetNoDataWarningMsg();
            noBookFoundMsg.Should().NotBeNullOrWhiteSpace();
            searchedOwnedBooks.Should().BeEmpty();
        }
        [TearDown]
        public void TearDown()
        {
            _bookService.DeleteCreatedBookFromStorage();
        }

        public async Task<string> GetToken(DeleteBookData dto)
        {
            if (DataStorage.GetData($"AccToken-{dto.UserId}") is null)
            {
                var res = await _accountService.TryHardGenerateTokenAsync(dto.Username, dto.Password);
                res.StatusCode.Should().Be(HttpStatusCode.OK);

                DataStorage.SetData($"AccToken-{dto.UserId}", "Bearer " + res.Data.Token);
            }
            return (string)DataStorage.GetData($"AccToken-{dto.UserId}");
        }
    }
}