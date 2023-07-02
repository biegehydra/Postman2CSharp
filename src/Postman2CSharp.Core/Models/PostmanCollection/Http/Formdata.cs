using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Postman2CSharp.Core.Infrastructure;
using Postman2CSharp.Core.Utilities;

namespace Postman2CSharp.Core.Models.PostmanCollection.Http;

public class Formdata : IFormData
{
    [JsonRequired] public required string Key { get; set; }
    [JsonRequired] public required string Type { get; set; }
    public FormDataType FormDataType => Type switch
    {
        "text" => FormDataType.Text,
        "file" => FormDataType.File,
        "default" => FormDataType.Text,
        _ => throw new NotImplementedException()
    };
    public JsonElement? Src { get; set; }
    public string? Value { get; set; }
    public string? Description { get; set; }
    public bool Disabled { get; set; }
    private string? _csPropertyName;
    public string CsPropertyName => _csPropertyName ??= Utils.NormalizeToCsharpPropertyName(Key);
}