using Postman2CSharp.Core.Utilities;
using System.Text.Json.Serialization;
using Postman2CSharp.Core.Infrastructure;

namespace Postman2CSharp.Core.Models.PostmanCollection.Http;

public class Parameter : IFormData
{
    [JsonRequired] public required string Key { get; set; }
    [JsonRequired] public required string Value { get; set; }
    public FormDataType FormDataType => FormDataType.Text;
    public string? Type { get; set; }
    public bool Disabled { get; set; }
    public string? Description { get; set; }
}