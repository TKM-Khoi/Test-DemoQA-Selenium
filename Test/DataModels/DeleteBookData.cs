using Newtonsoft.Json;

namespace Test.DataModels
{
    public class DeleteBookData
    {
        [JsonProperty("userId")]
        public string UserId { get; set; }
        [JsonProperty("username")]
        public string Username { get; set; }
        [JsonProperty("password")]
        public string Password { get; set; }
        [JsonProperty("isbn")]
        public string Isbn { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
    }
}