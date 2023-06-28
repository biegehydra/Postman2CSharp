using System.Text.Json.Serialization;

namespace Postman2CSharp.Core.Models.PostmanCollection.Http;

public class Parameter
{
    [JsonRequired] public required string Key { get; set; }
    [JsonRequired] public required string Value { get; set; }
    public string? Type { get; set; }
    public bool Disabled { get; set; }
    public string? Description { get; set; }

    private string? _csPropertyName;
    public string CsPropertyName => _csPropertyName ??= Helpers.NormalizeToCsharpPropertyName(Key);
}