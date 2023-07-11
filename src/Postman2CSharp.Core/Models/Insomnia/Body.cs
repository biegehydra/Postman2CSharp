using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Postman2CSharp.Core.Models.Insomnia
{
    public class Body
    {
        [JsonRequired]
        public required string MimeType { get; set; }

        [JsonRequired]
        public required string Text { get; set; }

        public string? FileName { get; set; }

        [JsonPropertyName("params")]
        [JsonRequired]
        public required List<Param> Parameters { get; set; }
    }
}
