using Newtonsoft.Json;

namespace Service.Models.Resquests
{
    public class CollectionOfIsbn
    {
        [JsonProperty("isbn")]
        public string isbn { get; set; }

        public CollectionOfIsbn(string isbn)
        {
            this.isbn = isbn;
        }
    }
}