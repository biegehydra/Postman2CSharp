using System.Text.Json.Serialization;

namespace Postman2CSharp.Core.Models.PostmanCollection.Authorization
{
    public class Apikey
    {
        [JsonRequired] public required string Key { get; set; }
        [JsonRequired] public required string Type { get; set; }
        public string? Value { get; set; }
    }
}