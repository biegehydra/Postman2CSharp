using System.Collections.Generic;
using System;
using System.Text.Json.Serialization;

namespace Postman2CSharp.Core.Models.Insomnia;

public class InsomniaCollection
{
    [JsonPropertyName("_type")]
    [JsonRequired]
    public required string Type { get; set; }

    [JsonPropertyName("__export_format")]
    [JsonRequired]
    public int ExportFormat { get; set; }

    [JsonPropertyName("__export_date")]
    [JsonRequired]
    public DateTime ExportDate { get; set; }

    [JsonPropertyName("__export_source")]
    [JsonRequired]
    public required string ExportSource { get; set; }

    [JsonRequired]
    public required List<Resource> Resources { get; set; }
}
