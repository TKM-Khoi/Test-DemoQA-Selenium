using Newtonsoft.Json;

namespace Test.DataModels
{
    public class RegisterData
    {
        [JsonProperty("firstName")]
        public string FirstName { get; set; }
        [JsonProperty("lastName")]
        public string LastName { get; set; }
        [JsonProperty("email")]
        public string? Email { get; set; }
        [JsonProperty("gender")]
        public string Gender { get; set; }
        [JsonProperty("phone")]
        public string Phone { get; set; }
        [JsonProperty("dateOfBirth")]
        public DateTime DateOfBirth { get; set; }
        [JsonProperty("subjects")]
        public string[]? Subjects { get; set; }
        [JsonProperty("hobbies")]
        public string[]? Hobbies{ get; set; }
        [JsonProperty("picture")]
        public string? Picture { get; set; }
        [JsonProperty("address")]
        public string? Address { get; set; }
        [JsonProperty("state")]
        public string? State { get; set; }
        [JsonProperty("city")]
        public string? City { get; set; }

    }
}