using Core.Client;
using Core.ShareData;

using RestSharp;

using Service.Const;
using Service.Models.Response;
using Service.Models.Resquests;

namespace Service.Services
{
    public class BookService
    {
        private readonly ApiClient _client;

        public BookService(ApiClient client)
        {
            _client = client;
        }
        public async Task<RestResponse<AddBookResponse>> AddBookWithTokenAsync(AddBookRequest data, string token)
        {
            return await _client.CreateRequest(ApiConst.ADD_BOOK_API)
                .AddHeader("accept", ContentType.Json)
                .AddHeader("Content-Type", ContentType.Json)
                .AddAuthorizationHeader(token)
                .AddBody(data)
                .ExecutePostAsync<AddBookResponse>();
        }
        public async Task<RestResponse<AddBookResponse>> AddBookWithUnameAndPasswordAsync(AddBookRequest data, string username, string password)
        {
            var response = await _client
                .SetBasicAuthentication(username, password)
                .CreateRequest(ApiConst.ADD_BOOK_API)
                .AddHeader("accept", ContentType.Json)
                .AddHeader("Content-Type", ContentType.Json)
                .AddBody(data)
                .ExecutePostAsync<AddBookResponse>();
            return response;
        }
        public async Task<RestResponse> DeleteBookWithTokenAsync(string userId, string isbn, string token)
        {
            return await _client
                .CreateRequest(ApiConst.DELETE_BOOK_APT)
                .AddHeader("accept", ContentType.Json)
                .AddHeader("Content-Type", ContentType.Json)
                .AddAuthorizationHeader(token)
                .AddBody(new
                {
                    userId = userId,
                    isbn = isbn
                })
                .ExecuteDeleteAsync();
        }
        public async Task<RestResponse> DeleteBookWithUnameAndPassAsync(string userId, string isbn, string username, string password)
        {
            return await _client
                .SetBasicAuthentication(username, password)
                .CreateRequest(ApiConst.DELETE_BOOK_APT)
                .AddHeader("accept", ContentType.Json)
                .AddHeader("Content-Type", ContentType.Json)
                .AddBody(new
                {
                    userId = userId,
                    isbn = isbn
                })
                .ExecuteDeleteAsync();
        }

        public void StoreBookCollectionToDeleteLater(string isbn, string creatorId, string creatorToken)
        {
            DataStorage.SetData(DataStorage.HAS_CREATED_BOOK, true);
            DataStorage.SetData(DataStorage.CREATED_BOOK_TOKEN(isbn, creatorId, isbn), new { isbn, creatorId, creatorToken });
        }
        public void StoreBookCollectionToDeleteLater(string isbn, string creatorId, string username, string password)
        {
            DataStorage.SetData(DataStorage.HAS_CREATED_BOOK, true);
            DataStorage.SetData(DataStorage.CREATED_BOOK_UNAME_PASS(isbn, creatorId, isbn), new { isbn, creatorId, username, password });
        }
        public void DeleteCreatedBookFromStorage()
        {
            if (DataStorage.GetData(DataStorage.HAS_CREATED_BOOK) != null && (Boolean)DataStorage.GetData(DataStorage.HAS_CREATED_BOOK))
            {
                foreach (var record in DataStorage.GetAllData())
                {
                    if (record.Key.StartsWith(DataStorage.CREATED_BOOK_TOKEN()))
                    {
                        dynamic created = record.Value;
                        DeleteBookWithTokenAsync(created.isbn, created.creatorId, created.creatorToken);
                    }
                    else if (record.Key.StartsWith(DataStorage.CREATED_BOOK_UNAME_PASS()))
                    {
                        dynamic created = record.Value;
                        RestResponse res = DeleteBookWithUnameAndPassAsync((string)created.creatorId, (string)created.isbn, (string)created.username, (string)created.password).Result;
                    }
                }
            }
        }


    }
}