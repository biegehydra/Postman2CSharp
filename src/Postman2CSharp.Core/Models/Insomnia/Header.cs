using System.Text.Json.Serialization;

namespace Postman2CSharp.Core.Models.Insomnia;

public class Header
{
    [JsonRequired]
    public required string Name { get; set; }

    [JsonRequired]
    public required string Value { get; set; }

    public string? Id { get; set; }

    public bool? Disabled { get; set; }
}
