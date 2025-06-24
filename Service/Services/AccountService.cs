using Core.Client;

using RestSharp;

using Service.Const;
using Service.Models.Response;
using Service.Models.Resquests;

namespace Service.Services
{
    public class AccountService
    {
         private ApiClient _client;

        public AccountService(ApiClient client)
        {
            _client = client;
        }
        /// <summary>
        /// This api of DEMOQA is broken(05.24)
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        /// 
        // public async Task<RestResponse<UserResponse>> GetDetailUserAsync(string userId, string token)
        // {
        //     return await _client.CreateRequest(ApiConstant.GetUserDetailApi(userId))
        //         .AddContentType(ContentType.Json)
        //         .AddAuthorizationHeader(token)
        //         .ExecuteGetAsync<UserResponse>();
        // }
        public async Task<RestResponse<TokenResponse>> GenerateTokenAsync(string username, string password)
        {
            TokenRequest tokenRequest = new()
            {
                Username = username,
                Password = password
            };
            return await _client.CreateRequest(ApiConst.GEN_JWT_TOKEN_API)
                .AddHeader("accept", ContentType.Json)
                .AddContentType(ContentType.Json)
                .AddJsonBody(tokenRequest)
                // .ExecutePostAsync<TokenResponse>();
                .ExecutePostAsync<TokenResponse>();
        }

        /// <summary>
        /// GenerateToken API somtimes does not work consistently. I will call that api until it works or past 5 times
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="allowedAttempts"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<RestResponse<TokenResponse>> TryHardGenerateTokenAsync(string username, string password, int allowedAttempts = 5)
        {
            int count = 1;
            RestResponse<TokenResponse> response = null;
            while (count <= allowedAttempts)
            {
                response = await GenerateTokenAsync(username, password);
                Console.WriteLine("AAAAAAA" + "" + response);
                Console.WriteLine("" + "");
                if (response.IsSuccessful)
                {
                    return response;
                }
                Thread.Sleep(4000);
                count++;
            }
            throw new Exception("Cant get token from API: " + response.Content);
        }
    }
}