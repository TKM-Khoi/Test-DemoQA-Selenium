using Newtonsoft.Json;

using Service.Models.Resquests;

namespace Service.Models.Response
{
    public class AddBookResponse
    {
        [JsonProperty("collectionOfIsbns")]
        public CollectionOfIsbn[] CollectionOfIsbns { get; set; }
    }
}