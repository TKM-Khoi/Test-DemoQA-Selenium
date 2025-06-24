using Newtonsoft.Json;

namespace Service.Models.Resquests
{
    public class AddBookRequest
    {
        public AddBookRequest(string userId, string isbn)
        {
            this.UserId = userId;
            CollectionOfIsbns = new CollectionOfIsbn[]{new CollectionOfIsbn(isbn)};
        }
        [JsonProperty("userId")]
        public string UserId { get; set; }
        [JsonProperty("collectionOfIsbns")]
        public CollectionOfIsbn[] CollectionOfIsbns { get; set; }
    }
}