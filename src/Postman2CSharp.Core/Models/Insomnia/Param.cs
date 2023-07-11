using Newtonsoft.Json;

namespace Postman2CSharp.Core.Models.Insomnia;

public class Param
{
    [JsonRequired]
    public required string Type { get; set; }

    [JsonRequired]
    public required string Name { get; set; }

    [JsonRequired]
    public required bool Disabled { get; set; }

    [JsonRequired]
    public required string FileName { get; set; }

    [JsonRequired]
    public required string Value { get; set; }
}
