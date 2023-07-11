using System.Text.Json.Serialization;

namespace Postman2CSharp.Core.Models.Insomnia
{
    public abstract class BaseResource
    {
        [JsonPropertyName("_type")]
        [JsonRequired]
        public required string Type { get; set; }

        [JsonPropertyName("_id")]
        [JsonRequired]
        public required string Id { get; set; }

        [JsonRequired]
        public required string? ParentId { get; set; }

        [JsonRequired]
        public required string Name { get; set; }

        [JsonRequired]
        public required long Modified { get; set; }

        [JsonRequired]
        public required long Created { get; set; }

        [JsonRequired]
        public long MetaSortKey { get; set; }
    }
}
