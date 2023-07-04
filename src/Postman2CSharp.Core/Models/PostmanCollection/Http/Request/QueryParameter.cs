using System.Text.Json.Serialization;
using Postman2CSharp.Core.Utilities;

namespace Postman2CSharp.Core.Models.PostmanCollection.Http.Request
{
    public class QueryParameter : ICsProperty
    {
        [JsonRequired] public required string Key { get; set; }
        public string? Value { get; set; }
        public string? Description { get; set; }
        public bool? Disabled { get; set; }
    }
}