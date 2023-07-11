using System.Collections.Generic;
using System;
using System.Text.Json.Serialization;

namespace Postman2CSharp.Core.Models.Insomnia;

public class InsomniaCollection
{
    [JsonPropertyName("_type")]
    [JsonRequired]
    public string Type { get; set; }

    [JsonPropertyName("__export_format")]
    [JsonRequired]
    public int ExportFormat { get; set; }

    [JsonPropertyName("__export_date")]
    [JsonRequired]
    public DateTime ExportDate { get; set; }

    [JsonPropertyName("__export_source")]
    [JsonRequired]
    public string ExportSource { get; set; }

    [JsonRequired]
    public List<Resource> Resources { get; set; }
}
