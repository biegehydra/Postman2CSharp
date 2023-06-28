using System.Text.Json.Serialization;

namespace Postman2CSharp.Core.Models.PostmanCollection
{
    public class CollectionInfo
    {
        // Needs to be "https://schema.getpostman.com/json/collection/v2.1.0/collection.json"
        [JsonRequired] public required string Schema { get; set; }
        [JsonRequired] public required string Name { get; set; }
        public string? PostmanId { get; set; }
        public string? Description { get; set; }
        public string? ExporterId { get; set; }
    }
}