using System.Text.Json.Serialization;

namespace Postman2CSharp.Core.Models.PostmanCollection
{
    public class CollectionVariable
    {
        [JsonRequired] public required string Key { get; set; }
        public string? Value { get; set; }
        public string? Type { get; set; }
    }
}